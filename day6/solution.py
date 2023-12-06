from functools import reduce

input_lines = open("input.txt", "r").readlines()

def get_line_value(line):
    return int("".join(list(filter(lambda x: x != "", line.strip().split(":")[1].split(' ')))))

time = get_line_value(input_lines[0])
record = get_line_value(input_lines[1])

possible_records = 0
for i in range(1, time):
    distance = (time - i) * i
    if distance > record:
        possible_records+=1

print(possible_records)
        
