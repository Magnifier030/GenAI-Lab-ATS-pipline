import itertools, re
from bs4 import BeautifulSoup

def levenshtein_distance(s1, s2):
    if len(s1) < len(s2):
        return levenshtein_distance(s2, s1)

    # len(s1) >= len(s2)
    if len(s2) == 0:
        return len(s1)

    previous_row = range(len(s2) + 1)
    for i, c1 in enumerate(s1):
        current_row = [i + 1]
        for j, c2 in enumerate(s2):
            insertions = previous_row[j + 1] + 1
            deletions = current_row[j] + 1
            substitutions = previous_row[j] + (c1 != c2)
            current_row.append(min(insertions, deletions, substitutions))
        previous_row = current_row

    return previous_row[-1]

def findTheMostSimilarWord( s1, strings ):
    # print(f"Final Text: {s1}, in {strings}")
    max_word = ""
    max_score = 10000000

    if len(strings) == 0:
        return ("Finish", "")
    for s2 in strings:
        score = levenshtein_distance(s1, s2)
        if score <= max_score:
            max_score = score
            max_word = s2
    
    if max_score > len(max_word) * 2:
        return (False, "")
    
    # print(f"Find key: {max_word}\n")
    
    return (True , max_word)

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

def abstract_keyword(content, target, tags = ['highlight', 'color']):
    soup = BeautifulSoup(content, 'lxml')
    criteria = soup.find_all('column')[target]
    tags = list(itertools.permutations(tags))

    datas = []
    for tag in tags:
        datas += (find_tag(criteria, list(tag), 2))
    
    if len(datas) == 0:
        criterias = criteria.find_all("color", recursive=False)
        criterias += criteria.find_all("highlight", recursive=False)
        return [sub.get_text() for sub in criterias]

    criterias = []
    for data in datas:
        text = data.get_text()
        if len(text) == 0:
            continue
        if text[0] == " ":
            text = text[1:]
        if not text:
            continue
        if text[-1] == " ":
            text = text[:-1]
        criterias.append(text)
    # print(len(str(soup.find('table'))))

    return criterias

def delete_command_from_criteria(color_criterias, commands):
    new_color_criterias = []
    for criteria in color_criterias:
        if criteria not in commands:
            new_color_criterias.append(criteria)
        # else:
        #     print("移除criteria")
    return new_color_criterias

def get_generative_data(instruction, data):
    color_tag_criterias = abstract_keyword(instruction, 3)
    color_tag_commands = abstract_keyword(instruction, 2)

    color_tag_criterias = [
        tag.strip()
        for tag in color_tag_criterias
        if len(tag) > 0
    ]

    # color_tag_criterias = [
    #     tag[1:] if len(tag) > 2 and tag[0] == " "  else 
    #     tag[:-2] if len(tag) > 2 and tag[-1] == " " else
    #     tag 
    #     for tag in color_tag_criterias
    # ]
    data = data[data.find('Answer') + 6 :] if "Answer" in data else data
    data = data[:data.find('User:')] if 'User:' in data else data

    soup = BeautifulSoup(data, 'lxml')
    commands = list(soup.find_all('command'))
    criterias = list(soup.find_all('criteria'))
    
    compared_commands = []
    compared_criterias = []
    for command in commands:
        command = command.text
            
        result, word = findTheMostSimilarWord(command, color_tag_commands)
        if not result:
            continue
        else:
            if result == "Finish":
                continue
        compared_commands.append(word)

    for criteria in criterias:
        
        criteria = criteria.text
        result, word = findTheMostSimilarWord(criteria, color_tag_criterias)

        if not result:
            continue
        else:
            if result == "Finish":
                continue
        compared_criterias.append(word)
        
    return compared_commands, compared_criterias