from functools import reduce

input_lines = open("input.txt", "r").readlines()

def get_line_values(line):
    return list(map(int, (filter(lambda x: x != "", line.strip().split(":")[1].split(' ')))))

times = get_line_values(input_lines[0])
distances = get_line_values(input_lines[1])

possible_records = []
for race in range(len(times)):
    record = distances[race]
    time = times[race]

    race_possible_records = 0
    for i in range(1, time + 1):
        distance = (time - i) * i
        if distance > record:
            race_possible_records+=1

    possible_records.append(race_possible_records)

sum_of_possible_records = reduce(lambda x, y: x*y, possible_records)
print(sum_of_possible_records)       
        
