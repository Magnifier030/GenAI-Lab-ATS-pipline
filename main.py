import json, shutil
from bs4 import BeautifulSoup

# from abstract import file_abstract, convert_to_dataset, convert_to_LLM_dataset
# from generate import generate
from create_document import create_document
from create_table import create_table
from analysis import get_generative_data
from create_program import main as create_program


def get_dataset(llm_dataset_path):
    with open(llm_dataset_path, 'r', encoding='utf-8') as llm_dataset:
        llm_dataset = json.load(llm_dataset)

    return llm_dataset

def get_generative_datas(generative_datas_path):
    with open(generative_datas_path, 'r', encoding='utf-8') as file:
        generative_datas = eval(file.read())

    generative_datas = generative_datas['Prompt 1']
    return generative_datas

# def get_generative_data(generative_data):
#     generative_data = generative_data[generative_data.find('Answer') + 6 :] if "Answer" in generative_data else generative_data
#     generative_data = generative_data[:generative_data.find('User:')] if 'User:' in generative_data else generative_data

#     soup = BeautifulSoup(generative_data, 'lxml')
#     generative_commands = list(soup.find_all('command'))
#     generative_criterias = list(soup.find_all('criteria'))

def get_conbined_datas(llm_dataset_path, generative_datas_path):
    llm_dataset = get_dataset(llm_dataset_path=llm_dataset_path)
    generative_datas = get_generative_datas(generative_datas_path=generative_datas_path)
    print(generative_datas)
    count = len(llm_dataset)
    datas = []

    for i in range(count):
        generative_data = generative_datas[i]
        generative_commands, generative_criterias = get_generative_data(
            instruction=llm_dataset[i]['instruction'],
            data=generative_data
        )

        data = {
            "test_station" : llm_dataset[i]['test_station'],
            "test_item" : llm_dataset[i]['test_item'],
            "instruction" : llm_dataset[i]['instruction'],
            "generative_commands" : generative_commands,
            "generative_criterias" : generative_criterias
        }

        datas.append(data)

    return datas

def main():
    file_name = str(input("請輸入 Test Plan 名稱: "))
    file_path = f"./files/{file_name}"
    folder_path = f"./files/{file_name}"

    # model_path = str(input("請輸入 Model 位置: "))
    # prompt_path = str(input("請輸入 Prompt 位置: "))
    output_path = f"{file_path}/generative_result.txt"

    # file_abstract(file_path)
    # convert_to_dataset(folder_path)
    # convert_to_LLM_dataset(folder_path, folder_path)
    # shutil.move(file_path, folder_path)
    
    llm_dataset_path = f"{file_path}/llm_datasets.json"
    generative_datas_path = output_path

    # generate(
    #     model_path=model_path,
    #     prompt_path=prompt_path,
    #     output_path=output_path,
    #     dataset_path=llm_dataset_path
    # )

    datas = get_conbined_datas(
        llm_dataset_path=llm_dataset_path,
        generative_datas_path=generative_datas_path
    )
    
    docx_path = f"{file_path}/compared.docx"
    json_path = f"{file_path}/test_table.json"

    create_table(
        datas=datas,
        output_file_path=json_path
    )

    create_document(
        datas=datas,
        output_file_path=docx_path
    )

    create_program(
        file_name=file_name
    )

if __name__ == '__main__':
    main()