import json

def create_table(datas:list, output_file_path:str):
    with open(output_file_path, "w", encoding='utf-8') as file:
        json.dump(datas, file, indent=4)


    
