input_lines = open("input.txt", "r").readlines()

def get_calibration_value(line):
    all_digits = list(filter(lambda c: c.isdigit(), line))
    usable_digits = all_digits[0] + all_digits[0 if len(all_digits) == 1 else len(all_digits) - 1]
    return int(usable_digits)

calibration_values = list(map(get_calibration_value, input_lines))

print(sum(calibration_values))
