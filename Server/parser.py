#Parse log files into linked list with node strucutre as follows
#f = open("test_keyboard_log.txt", "r")

def parse_into_list(file):
    log = []
    line = file.readline()
    while line != "":
        time_and_state = line.split(' ')
        date = time_and_state[0]
        time = time_and_state[1]
        state = time_and_state[2]
        if state[-1] == '\n':
            state = state[:-1]
        node = [date,time,state]
        log.append(node)
        line = file.readline()
    return log

#print(parse_into_list(f))