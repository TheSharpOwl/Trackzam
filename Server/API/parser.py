#Parse log files into linked list with node strucutre as follows
#f = open("test_keyboard_log.txt", "r")

def parse_into_list(file):
    log = []
    for byte_line in file:
        line = byte_line.decode("utf-8")

        time_and_state = line.split(' ')
        date = time_and_state[0]
        time = time_and_state[1]
        state = time_and_state[2]
        if state[-2:] == '\r\n':
            state = state[:-2]
        elif state[-1] == '\n':
            state = state[:-1]

        date_split = date.split("/")
        date = date_split[2]+'-'+date_split[1]+'-'+date_split[0]

        node = {'date':date,'time':time,'state':state}
        log.append(node)
    return log

#print(parse_into_list(f))