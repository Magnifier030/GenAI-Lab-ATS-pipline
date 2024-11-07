import zipfile, os
from docx import Document
import xml.etree.ElementTree as ET
import json, random

namespaces = {
    'w': 'http://schemas.openxmlformats.org/wordprocessingml/2006/main'
}
styles_dict = None

def read_docx(file_path):
    doc = Document(file_path)
    return doc

def convert_to_xml(doc):
    xml_str = doc._element.xml
    return xml_str

def save_xml(xml_str, output_file):
    with open(output_file, 'w', encoding='utf-8') as f:
        f.write(xml_str)

def extract_styles_xml(docx_file, output_file):
    with zipfile.ZipFile(docx_file, 'r') as docx:
        with docx.open('word/styles.xml') as styles_file:
            styles_content = styles_file.read()
            with open(output_file, 'wb') as f:
                f.write(styles_content)

def extract_styles_and_levels(file_path):
    tree = ET.parse(file_path)
    root = tree.getroot()

    styles = {}
    for style in root.findall('w:style', namespaces):
        style_id = style.get('{http://schemas.openxmlformats.org/wordprocessingml/2006/main}styleId')
        outline_lvl_elem = style.find('w:pPr/w:outlineLvl', namespaces)
        if outline_lvl_elem is not None:
            outline_lvl = outline_lvl_elem.get('{http://schemas.openxmlformats.org/wordprocessingml/2006/main}val')
            styles[style_id] = outline_lvl

    return styles

# 擷取文字並保留換行
def extract_text(element):
    texts = []
    wval = None
    color_texts = []
    
    for node in element:
        color_val = None
        highlight_val = None
        if node.tag == '{http://schemas.openxmlformats.org/wordprocessingml/2006/main}pPr':
            for subnode in node:
                if subnode.tag == '{http://schemas.openxmlformats.org/wordprocessingml/2006/main}pStyle':
                    wval = subnode.attrib.get('{http://schemas.openxmlformats.org/wordprocessingml/2006/main}val')
                    break
                elif subnode.tag == '{http://schemas.openxmlformats.org/wordprocessingml/2006/main}outlineLvl':
                    wval = str(int(subnode.attrib.get('{http://schemas.openxmlformats.org/wordprocessingml/2006/main}val'))+1)
                    break
                    
        elif node.tag == '{http://schemas.openxmlformats.org/wordprocessingml/2006/main}r':
            text_parts = []
            delete_line = None
            for subnode in node:
                print([sub for sub in subnode])
                if subnode.tag == '{http://schemas.openxmlformats.org/wordprocessingml/2006/main}rPr':
                    color = subnode.find('.//w:color', namespaces)
                    if color is not None:
                        color_val = color.attrib.get('{http://schemas.openxmlformats.org/wordprocessingml/2006/main}val')
                    highlight = subnode.find('.//w:highlight', namespaces)
                    if highlight is not None:
                        highlight_val = highlight.attrib.get('{http://schemas.openxmlformats.org/wordprocessingml/2006/main}val')
                    
                    delete_line = subnode.find('.//w:strike', namespaces)
                if subnode.tag == '{http://schemas.openxmlformats.org/wordprocessingml/2006/main}t' and delete_line is None:
                    print("text")
                    text_parts.append(subnode.text)
            
            text = ''.join(text_parts)
            print(F"text:{text}")
            if text == '':
                print("略過")
                continue
            color_texts.append(
                {
                    'color' : f"'#{color_val};'" if color_val else None,
                    'highlight' : f"'{highlight_val}'" if highlight_val else None,
                    'text' : text
                }
            )

            
        elif node.tag == '{http://schemas.openxmlformats.org/wordprocessingml/2006/main}cr':
            color_texts.append(
                {
                    'color' : f"'#{color_val};'" if color_val else None,
                    'highlight' : f"'{highlight_val}'" if highlight_val else None,
                    'text' : "<highlight value = None><color value = None>\n</highlight></color>"
                }
            )
    
    # print(color_texts)
    final_text = []
    last_color_index = 0
    last_highlight_index = 0
    for i in range(len(color_texts)):
        # print(i,  color_texts[i])
        text = ""
        color_key = False
        highlight_key = False
        color_all = False
        highlight_all = False
        add_key = False
        final_key = False
        # if color_texts[i]['color'] == None:
        #     last_color_index = i + 1
        #     add_key = True
        # if color_texts[i]['highlight'] == None:
        #     last_highlight_index = i + 1
        #     add_key = True

        # if add_key:
        #     final_text.append([
        #         [i],
        #         color_texts[i]['text']
        #     ])
        if i + 1 < len(color_texts) and not add_key:
            if color_texts[i]['color'] != color_texts[i+1]['color']:
                # print(f"{color_texts[i]['color']} != {color_texts[i+1]['color']}")
                # print(f"last_color : {last_color_index}\nlast_highlight : {last_highlight_index}")
                if last_color_index > last_highlight_index:
                    
                    # print("直接新增 color")
                    text =  f"<color value = {color_texts[i]['color']}>{''.join([c['text'] for c in color_texts[last_color_index:i+1]])}</color>"
                    final_text.append([
                        [i for i in range(last_color_index, i+1)],
                        text
                    ])
                    last_color_index = i + 1

                elif last_color_index == last_highlight_index:
                    color_key = True
                else:
                    color_all = True
            if color_texts[i]['highlight'] != color_texts[i+1]['highlight']:
                # print(f"{color_texts[i]['highlight']} != {color_texts[i+1]['highlight']}")
                if last_highlight_index > last_color_index:
                    # print("直接新增 highlight")
                    text += f"<highlight value = {color_texts[i]['highlight']}>{''.join([c['text'] for c in color_texts[last_highlight_index:i+1]])}</highlight>"
                    final_text.append([
                        [i for i in range(last_highlight_index, i+1)],
                        text
                    ])
                    last_highlight_index = i + 1
                    
                elif last_color_index == last_highlight_index:
                    highlight_key = True
                else:
                    highlight_all = True
        elif not add_key:
            final_key = True
            if last_color_index > last_highlight_index:
                # print("進入 color")
                text =  f"<color value = {color_texts[i]['color']}>{''.join([c['text'] for c in color_texts[last_color_index:i+1]])}</color>"
                final_text.append([
                    [i for i in range(last_color_index, i+1)],
                    text
                ])
                last_color_index = i + 1
            elif last_color_index == last_highlight_index:
                color_key = True
            else:
                color_all = True

            if last_highlight_index > last_color_index:
                # print("進入 highlgiht")
                text += f"<highlight value = {color_texts[i]['highlight']}>{''.join([c['text'] for c in color_texts[last_highlight_index:i+1]])}</highlight>"
                final_text.append([
                    [i for i in range(last_highlight_index, i+1)],
                    text
                ])
                last_highlight_index = i + 1
            elif last_color_index == last_highlight_index:
                highlight_key = True
            else:
                highlight_all = True

        if highlight_key or color_key:
            if highlight_key and color_key:
                # print("新增內部共同")
                text = f"<highlight value = {color_texts[i]['highlight']}><color value = {color_texts[i]['color']}>{''.join([c['text'] for c in color_texts[last_highlight_index:i+1]])}</color></highlight>"
                final_text.append([
                    [i for i in range(last_color_index, i+1)],
                    text
                ])
                last_color_index = i+1
                last_highlight_index = i+1
            elif highlight_key:
                # print("新增內部 highlight")
                text = f"<highlight value = {color_texts[i]['highlight']}>{''.join([c['text'] for c in color_texts[last_highlight_index:i+1]])}</highlight>"
                
                final_text.append([
                    [i for i in range(last_highlight_index, i+1)],
                    text
                ])
                last_highlight_index = i+1
            else:
                # print("新增內部 color")
                text = f"<color value = {color_texts[i]['color']}>{''.join([c['text'] for c in color_texts[last_highlight_index:i+1]])}</color>"
                final_text.append([
                    [i for i in range(last_color_index, i+1)],
                    text
                ])
                last_color_index = i+1
                

        elif color_all:
            # print("進入 color all")
            # print(f"last : {last_color_index}")
            text_temp = []
            new_final_text = []
            # print(final_text)
            for text in final_text:
                if last_color_index in text[0] or last_color_index < text[0][0]:
                    text_temp.append(text)
                else:
                    new_final_text.append(text)
            final_text = new_final_text.copy()
            # print(f"text_temp : {text_temp}")
            now_text = f"<highlight value = {color_texts[i]['highlight']}>{color_texts[i]['text']}</highlight>" if not final_key else ""
            text = f"<color value = {color_texts[i]['color']}>{''.join([t[1] for t in text_temp])}{now_text}</color>"
            final_text.append([
                [i for i in range(last_color_index, i)],
                text
            ])

            last_color_index = i+1
            
            

        elif highlight_all:
            # print("進入 highlight all")

            text_temp = []
            new_final_text = []
            # print(final_text)
            # print(f"last : {last_color_index}")
            for text in final_text:
                if last_highlight_index in text[0] or last_highlight_index < text[0][0]:
                    text_temp.append(text)
                else:
                    new_final_text.append(text)
            now_text =f"<color value = {color_texts[i]['color']}>{color_texts[i]['text']}</color>" if not final_key else ""
            text = f"<highlight value = {color_texts[i]['highlight']}>{''.join([t[1] for t in text_temp])}{now_text}</highlight>"
            final_text = new_final_text.copy()
            final_text.append([
                [i for i in range(last_highlight_index, i)],
                text
            ])
            last_highlight_index = i+1
            
            
            
        # print(f"final_text : {final_text}")
    
    # print(wval)
    # print(styles_dict)
    output_text = "".join([t[1] for t in final_text])
    return f"<level = {styles_dict[wval]}>{output_text}</level>" if wval and wval in styles_dict.keys() else output_text

# 提取表格
def extract_table(element):
    rows = []
    for row in element:
        if row.tag not in ['{http://schemas.openxmlformats.org/wordprocessingml/2006/main}tr']:
            continue
        columns = []
        for column in row:
            if column.tag in ['{http://schemas.openxmlformats.org/wordprocessingml/2006/main}tc']:
                texts = []
                process_element(column, texts)
                if not texts:
                    continue
                columns.append('<highlight value = None><color value = None>\n</highlight></color>'.join(texts))
        rows.append(columns)
    return rows

# 處理每個 element
def process_element(element, texts):
    for child in element:
        if child.tag in ['{http://schemas.openxmlformats.org/wordprocessingml/2006/main}p']:
            text = extract_text(child)
            if text.strip():
                texts.append(text)
        elif child.tag in ['{http://schemas.openxmlformats.org/wordprocessingml/2006/main}tbl']:
            text = extract_table(child)
            texts.append(text)
            continue
        if len(list(child)) > 0:
            process_element(child, texts)

def levelFind(level, text_lines):
    lines = []
    last_index = None
    last_station = None
    for i in range(len(text_lines)):
        if f'<level = {level}>' in text_lines[i]:
            s = text_lines[i].split('>')
            t = s[len(s)//2]
            t = t.split("<")[0]

            if last_index is None:
                last_index = i
                last_station = t
            else:
                lines.append(
                    (last_station, levelFind(level + 1, text_lines[last_index+1 : i]))
                )
                last_station = t
                last_index = i
        elif last_station is not None:
            continue
        else:
            lines.append(text_lines[i])
    if last_index:
        lines.append(
            (last_station, levelFind(level + 1, text_lines[last_index+1 : ]))
        )
    return lines

def process_table_list(table, test_station):
    # print(table)
    init_index = 0
    test_items = []

    if len(table[0]) == 1:
        temp_station = str(table[0]).split('<')
        test_station = temp_station[len(temp_station)//2].split('>')[-1]
        init_index += 1
        if len(table[1]) != 3:
            return []
    else:
        return []
    
    print(table)
    
    heading = f"<heading-row>{''.join([f'<column>{text}</column>' for text in table[init_index][1:]])}</heading-row>"
    
    for row in table[init_index+1:]:
        if not row:
            continue
        content = f"<row>{''.join([f'<column>{text}</column>' for text in row[1:]])}</row>"

        commandAndCriteria = f"<table>{heading}{content}</table>"
        test_item = str(row[0]).split('<')
        test_item = test_item[len(test_item)//2].split('>')[-1]

        if test_station and test_item:


            item = {
                'test_station' : test_station,
                'test_item' : test_item,
                'commandAndCriteria' : commandAndCriteria
            }

            test_items.append(item)
    return test_items

def level2items(items, last_station = None, last_item = None):
    input = []
    temp = []
    if len(items) == 0:
        return input
    
    for item in items:
        if type(item) is tuple:
            # print(item)
            input += level2items(item[1], last_item, item[0])
        elif type(item) is list and type(item[0]) is list:
            input += process_table_list(item, last_item)
        # else:
        #     temp.append(item)
    
    if temp:
        item = {
            'test_station' : last_station,
            'test_item' : last_item,
            'commandAndCriteria' : '<highlight value = None><color value = None>\n</highlight></color>'.join(temp)
        }

        input.append(item)

    return input

def file_abstract(file_name):


    # 读取Python-docx文件
    docx_file = f'{file_name}.docx'
    doc = read_docx(docx_file)

    os.makedirs(
        file_name,
        exist_ok=True
    )

    # 将文件内容转换为XML格式
    xml_str = convert_to_xml(doc)

    # 保存为XML文件
    xml_file = f'{file_name}/docx.xml'
    save_xml(xml_str, xml_file)

    # 提取 styles.xml 文件并保存为 XML 文件
    styles_xml_file = f'{file_name}/styles.xml'
    extract_styles_xml(docx_file, styles_xml_file)

    print(f'{xml_file} 和 {styles_xml_file} 已成功保存.')

    # 調用函數並打印結果
    styles_file = f'{file_name}/styles.xml'
    global styles_dict
    styles_dict = extract_styles_and_levels(styles_file)
    print(styles_dict)
    
    # 讀取 XML 檔案
    tree = ET.parse(f'{file_name}/docx.xml')
    root = tree.getroot()
    
    # 處理 body 中的每個元素
    text_lines = []
    body = root.find('w:body', namespaces)
    process_element(body, text_lines)

    # 將結果輸出
    # for text in text_lines:
    #     print(text)

    # 將結果寫入文件
    with open(f'{file_name}/output.txt', 'w', encoding='utf-8') as f:
        for text in text_lines:
            f.write(f"{str(text)}\n")
    print("轉換完畢！")

    items = levelFind(0, text_lines)
    test_items = level2items(items)

    target = {
        'target' : {
            'commands' :[],
            'criterias': []
        },
        "level" : ""
    }
    test_items = [{**test_item, **target} for test_item in test_items]

    json_file_path = f'./{file_name}/datas.json'

    # 将列表转换为 JSON 并保存到文件
    with open(json_file_path, 'w') as json_file:
        json.dump(test_items, json_file, indent=4)

    print(f"資料已成功保存為 JSON 文件: {json_file_path}")

def convert_to_dataset(file_name):
    json_file_path = f"./{file_name}/datas.json"
    new_test_items = []
    with open(json_file_path, 'r', encoding='utf-8') as json_file:
        test_items = json.load(json_file)
        
        for test_item in test_items:
            commandAndCriteria = test_item['commandAndCriteria']
            commands = test_item['target']['commands']
            criterias = test_item['target']['criterias']
            test_item['target_index'] = {
                'commands' : [],
                'criterias' : []
            }

            for targets, key in [(commands, 'commands'), (criterias, 'criterias')]:
                for target in targets:
                    try:
                        start = commandAndCriteria.index(target)
                        # print(target)
                        end = start + len(target)
                    except:
                        test_item['target_index'][key].append(
                            (None, None)
                        )
                    test_item['target_index'][key].append(
                        (start, end)
                    )
        
            new_test_items.append(test_item)
    
    new_json_file_path = f"./{file_name}/datasets.json"
    with open(new_json_file_path, 'w') as json_file:
        json.dump(new_test_items, json_file, indent=4)

    print(f"資料已成功保存為 JSON 文件: {new_json_file_path}")

from bs4 import BeautifulSoup        
def convert_to_LLM_dataset(file_name, new_file_path):
    file_path = f"./{file_name}/datasets.json"
    new_test_items = []
    new_test_items_for_augumentation = []

    with open(file_path, 'r', encoding='utf-8') as json_file:
        items = json.load(json_file)
        for item in items:
            commands = "\n".join([f"<command>{BeautifulSoup(command, 'lxml').get_text()}</command>" for command in item['target']['commands']])
            criterias = "\n".join([f"<criteria>{BeautifulSoup(criteria, 'lxml').get_text()}</criteria>" for criteria in item['target']['criterias']])
            test_item = {
                "instruction" : f"{item['commandAndCriteria']}",
                "input": "",
                "output": f'Answer: \n{commands}\n{criterias}<|end_of_text|>',
                'history' : []
            }
            aug_test_item = {
                "instruction" : f"{item['commandAndCriteria']}",
                "input": "",
                "output": f'Answer: \n{commands}\n{criterias}<|end_of_text|>',
                'history' : [],
                'target' : item['target'],
                'test_station' : item['test_station'],
                'test_item' : item['test_item'],
                'level' : item['level'] if 'level' in item.keys() else ""
            }

            new_test_items.append(
                test_item
            )
            new_test_items_for_augumentation.append(
                aug_test_item
            )

    print(f"共有 {len(new_test_items)} 個 Test Items.")
    with open(f"{new_file_path}/llm_datasets.json", 'w+') as json_file:
        json.dump(new_test_items_for_augumentation, json_file, indent=4)

    print(f"資料已成功轉換成 LLM 格式，並保存為 JSON 文件: {new_file_path}")

def conbine_all_datasets(file_name = "datasets", should_split_to_be_trainAndTest = False, ratio = 0.6):
    new_test_items = []
    new_file_path = f"./{file_name}"

    for root, dirs, files in os.walk(new_file_path):
        for file in files:
            if file in ['test_dataset.json', 'train_dataset.json', 'dataset_for_aug.json', "test_dataset_level_0.json", "test_dataset_level_1.json", "test_dataset_level_2.json", "test_dataset_level_3.json"]:
                continue
            file_path = os.path.join(root, file)
            with open(file_path, 'r', encoding='utf-8') as json_file:
                items = json.load(json_file)
                print(f"{file} have {len(items)} test items.")
                new_test_items += items
    
    random.shuffle(new_test_items)
    new_test_items_for_aug = new_test_items.copy()
    new_test_items = [{k: v for k, v in item.items()} for item in new_test_items]

    if should_split_to_be_trainAndTest:
        train_ratio = int(len(new_test_items) * ratio)

        train_dataset = new_test_items[:train_ratio]
        test_dataset = new_test_items[train_ratio:]

        with open(f"{new_file_path}/train_dataset.json", 'w+' ,encoding='utf-8') as json_file:
            json.dump(train_dataset, json_file, indent=4, ensure_ascii=False)
        
        with open(f"{new_file_path}/test_dataset.json", 'w+' ,encoding='utf-8') as json_file:
            json.dump(test_dataset, json_file, indent=4, ensure_ascii=False)

        

        with open(f"{new_file_path}/dataset_for_aug.json", 'w+' ,encoding='utf-8') as json_file:
            json.dump(new_test_items_for_aug[:train_ratio], json_file, indent=4, ensure_ascii=False)

        print(f"train dataset have {len(train_dataset)} test items.")
        print(f"test dataset have {len(test_dataset)} test items.")
    else:
        with open(f"{new_file_path}/dataset.json", 'w+') as json_file:
            json.dump(new_test_items, json_file, indent=4, ensure_ascii=False)
        print(f"dataset have {len(new_test_items)} test items.")

def split_train_dataset_by_level(level_nums : dict):
    file_path = "./datasets/train_dataset.json"
    with open(file_path, 'r', encoding='utf-8') as json_file:
        items = json.load(json_file)

    all_selected_items = []
    
    for level, num in level_nums.items():
        temp_items = [item for item in items if item['level'] == level]
        abstracted_num = num if len(temp_items) > num else len(temp_items)

        final_items = random.sample(temp_items, abstracted_num)

        save_file_path = f"./datasets/test_dataset_level_{level}.json"
        with open(save_file_path, 'w+', encoding='utf-8') as file:
            json.dump(final_items, file, indent=4, ensure_ascii=False)
        print(f"level {level} 的 test_dataset 共有 {abstracted_num} 個 items. 並儲存於 {save_file_path}.")

        all_selected_items.extend(final_items)
    
    remaining_items = [item for item in items if item not in all_selected_items]
    
    # 將剩餘的 items 保存回 train dataset
    with open(file_path, 'w', encoding='utf-8') as json_file:
        json.dump(remaining_items, json_file, indent=4, ensure_ascii=False)
    
    print(f"更新後的 train_dataset 共有 {len(remaining_items)} 個 items.")
