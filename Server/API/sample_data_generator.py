import random
import string

def gen_action_k():
    return random.choice(string.ascii_letters)


def gen_list_k(dur_h, start_h, start_m, start_s, date):
    secs = 0 #start_s
    mins = 0 #start_m
    hours = 0 #start_h
    out = []
    while (hours*360 + mins*60 + secs) < dur_h*60*60:
        node = [date, hours, mins, secs, gen_action_k()]
        out.append(node)
        secs += 1
        if secs == 60:
            secs = 0
            mins += 1
            if mins == 60:
                mins = 0
                hours += 1
                if hours == 24:
                    print("pisos") 
    return out

def print_doc(Name, out):
    f = open("test_keyboard_log.txt", "w")
    for i in out:
        d = i[0]
        if i[1]<10:
            h = '0' + str(i[1])
        else:
            h = str(i[1])
        if i[2]<10:
            m = '0' + str(i[1])
        else:
            m = str(i[1])
        if i[3]<10:
            s = '0' + str(i[1])
        else:
            s = str(i[1])
        a = i[4]
        line = d + ' ' + h + ':' + m + ':' + s + ' ' + a + '\n'
        f.write(line)
    f.close

print_doc('test.txt', gen_list_k(1.5, 12,30,00,'06/02/2021'))
#print(gen_list_k(0.01, 12,30,00,'06/02/2021'))
