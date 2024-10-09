from transformers import AutoTokenizer, AutoModelForCausalLM, pipeline, BitsAndBytesConfig
import json
import pandas as pd
import torch
from tqdm import tqdm
from bs4 import BeautifulSoup
import itertools

# 初始化消息
messages = [
    {"role": "system", "content": "You are a helpful assistant!"},
    {"role": "user", "content": "Hey how are you doing today?"},
]

def load_model(model_path:str, device_map:str = "auto"):
    bnb_config = BitsAndBytesConfig(
        load_in_8bit=True,
        bnb_4bit_use_double_quant=True,   # 如果你需要使用雙重量化，可以設置這個參數
        bnb_4bit_quant_type="nf4",        # 根據需求設置量化類型
        bnb_4bit_compute_dtype=torch.float16,  # 設置計算數據類型
    )

    # 加载模型和分词器
    device = torch.device("cuda" if torch.cuda.is_available() else "cpu")
    tokenizer = AutoTokenizer.from_pretrained(model_path)
    print("取得 tokenizer")

    model = AutoModelForCausalLM.from_pretrained(model_path, quantization_config=bnb_config, device_map = device_map)
    print("取得 model")

    # 初始化 pipeline 并指定使用 GPU
    chat_pipeline = pipeline(
        'text-generation', 
        model=model, 
        tokenizer=tokenizer,
    )
    print("取得 pipeline")

    return chat_pipeline


def find_tag(content: BeautifulSoup, tags: list, times):
    tag_name = tags[0]
    datas = []
    for sub in content.find_all(tag_name, recursive=False):
        if sub.get('value') != 'None' and sub.get('value') != '#000000;':
            datas.append(sub)
        else:
            if times != 1:
                for data in find_tag(sub, tags[1:], times - 1):
                    datas.append(data)
    return datas

def abstract_keyword(content, tags=['highlight', 'color']):
    soup = BeautifulSoup(content, 'lxml')
    key = True

    # 第一次修改 criteria
    criteria = soup.find_all('column')[3]
    tag_permutations = list(itertools.permutations(tags))
    datas = []
    
    for tag in tag_permutations:
        datas += find_tag(criteria, list(tag), 2)

    if len(datas) != 0:
        key = False
        criteria.clear()
        for data in datas:
            criteria.append(data)
            criteria.append(BeautifulSoup('<color value="None">\\n</color>', 'lxml').find('color'))
    
    # 打印出修改後的 criteria，檢查是否更新
    # print("Updated criteria:")
    # print(criteria)

    # 第二次修改 command
    command = soup.find_all('column')[2]
    datas = []
    
    for tag in tag_permutations:
        datas += find_tag(command, list(tag), 2)

    if len(datas) != 0:
        key = False
        command.clear()
        for data in datas:
            command.append(data)
            command.append(BeautifulSoup('<color value="None">\\n</color>', 'lxml').find('color'))
    
    if key:
        return content

    return str(soup.find('table'))

# 定义生成响应的函数
def generate_response(messages, chat_pipeline):
    prompt = chat_pipeline.tokenizer.apply_chat_template(
        messages,
        tokenize=False,
        add_generation_prompt=True
    )
    
    terminators = [
        chat_pipeline.tokenizer.eos_token_id,
        chat_pipeline.tokenizer.convert_tokens_to_ids('<|end_of_text|>'),
        # chat_pipeline.tokenizer.convert_tokens_to_ids('<|eot_id|>'),
    ]
    
    try:
        outputs = chat_pipeline(
            prompt,
            max_new_tokens=2000,
            eos_token_id=terminators,
            do_sample=True,
            temperature=0.6,
            top_p=0.9,
        )
        
        response = outputs[0]["generated_text"][len(prompt):]
    except Exception as ex:
        print("【error】", ex)
        return ""
    return response


def generate(model_path, prompt_path, output_path, dataset_path):
    chat_pipeline = load_model(model_path=model_path)
    # 初始化结果字典
    result = {}
    prompts = pd.read_json(prompt_path)[0].tolist()  # 將 DataFrame 轉換為列表
    with open(dataset_path, 'r') as file:
        datas = json.load(file)

    print("取得 prompts")

    # 生成响应并记录结果
    index = 0
    for prompt in prompts:
        index += 1
        result[f'Prompt {index}'] = []
        for data in tqdm(datas, desc="Processing items"):
            messages[0]['content'] = prompt
            messages[1]['content'] = f"<content>{abstract_keyword(data['instruction'])}</content>"
            response = generate_response(messages, chat_pipeline)
            result[f'Prompt {index}'].append(response)
            print(f"response: {response}")

    # 將結果保存到 JSON 文件
    with open(output_path, 'w') as file:
        json.dump(result, file, indent=4, ensure_ascii=False)

    print(f"結果已保存至 {output_path}")
