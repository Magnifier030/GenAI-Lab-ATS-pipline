import json, os
from datetime import datetime

def set_function(function:str, test_item:str, test_command:list, pass_criteria:list):
    def CommandCheck_OneByOne(function:str, test_item:str, commands:list, criterias:list):
        functions = []
        index = 0 
        for command, criteria in zip(commands, criterias):
            function_text = function.replace("[test_item]", test_item)
            function_text = function_text.replace("[test_command]", command)
            function_text = function_text.replace("[pass_criteria]", criteria)
            function_text = f"testResult = {function_text}" if index == 0 else f"testResult = testResult && {function_text}"
            
            index+=1
            functions.append(function_text)

        function = "".join(functions)
        return function
    
    def CommandCheck_OneByMuti(function:str, test_item:str, commands:list, criterias:list):
        criterias_text = ", ".join([
            f'"{criteria}"' for criteria in criterias
        ])
        function_text = function.replace("[test_item]", test_item)
        function_text = function_text.replace("[test_command]", commands[0])
        function_text = function_text.replace("[pass_criteria]", criterias_text)
        function_text = f"testResult = {function_text}"
        
        return function_text
    
    def SelectFunction(commands:list, criterias:list):
        num_of_commands = len(commands)
        num_of_criterias = len(criterias)

        if num_of_commands == num_of_criterias:
            return "CommandCheck_OneByOne"

        elif num_of_commands == 1:
            return "CommandCheck_OneByMuti"

    functions = {
        "CommandCheck_OneByOne" : CommandCheck_OneByOne,
        "CommandCheck_OneByMuti" : CommandCheck_OneByMuti
    }
    
    function_type = SelectFunction(test_command, pass_criteria)
    function = functions[function_type](
        function=function,
        test_item=test_item,
        commands=test_command,
        criterias=pass_criteria
    )
    return function
        
    
def set_program(function:str, test_station:str, test_item:str, test_command:list, pass_criteria:list, template:str):
    result = True

    # if len(test_command) != len(pass_criteria):
    #     print("command 與 criteria 數量不匹配，不符合模版")
    #     return result, ""
    try:
        test_function = set_function(
            function=function,
            test_item=test_item,
            test_command=test_command,
            pass_criteria=pass_criteria
        )
        test_program = template.replace("[test_station]", test_station)
        test_program = test_program.replace("[test_item]", test_item)
        test_program = test_program.replace("[test_function]", test_function)
        
    except Exception as ex:
        print(f"讀取 data 成 test table 發生問題: {ex}")
        result = False
        return result, ""

    return result, test_program

def init(file_name:str):
    with open(f'./constent/templates.txt', 'r') as data_file:
        template = str(data_file.read())

    with open(f'./files/{file_name}/generative_test_table.json', 'r') as data_file:
        test_items = json.load(data_file)


    return template, test_items

def save_programs(test_programs:list, type:str):
    folder_path = f"./files/{file_name}/"
    function = None

    def save_normally(test_programs, file):
        for _, _, test_program in test_programs:
            file.write(test_program)

        return file
    
    def save_llm_dataset(test_programs, file):
        datas = []
        for instruction, CoT, test_program, prompt in test_programs:
            data = {
                "instruction": instruction,
                "input": prompt,
                "output": f"Answer:{CoT}<test_program>{test_program}</test_program><|end_of_text|>",
                "history": [],
            }

            datas.append(data)

        json.dump(datas, file, indent=4)

        return file
    
    if type == "LLM":
        file_name = "llm_dataset.json"
        function = save_llm_dataset

    elif type == "NORMAL":
        file_name = "test_program.cs"
        function = save_normally

    save_path = f"{folder_path}/{file_name}"
    with open(save_path, "w", encoding='utf-8') as file:
        function(test_programs, file)
        file.close()

    print("儲存成功！")

def main(file_name:str):
    # init
    template, test_items = init(file_name=file_name)

    
    test_programs = []
    for test_station, test_item, test_commands, pass_criterias in test_items:
        
        test_program = set_program(
            function="Command_Check",
            test_station=test_station,
            test_item=test_item,
            test_command=test_commands,
            pass_criteria=pass_criterias,
            template=template
        )

        test_programs.append(test_program)
    
    save_programs(
        test_programs=test_programs,
        type="NORMAL"
    )