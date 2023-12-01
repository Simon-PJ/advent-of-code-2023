input_lines = open("input.txt", "r").readlines()

word_digits = ['one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine']

def get_calibration_value(line):
    all_digits = []
    for i in range(len(line)):
        for word in word_digits:
            if ''.join(line[i:]).startswith(word):
                all_digits.append(str(word_digits.index(word) + 1))
        if line[i].isdigit():
            all_digits.append(line[i])

    usable_digits = all_digits[0] + all_digits[0 if len(all_digits) == 1 else len(all_digits) - 1]
    return int(usable_digits)

calibration_values = list(map(get_calibration_value, input_lines))

print(sum(calibration_values))
