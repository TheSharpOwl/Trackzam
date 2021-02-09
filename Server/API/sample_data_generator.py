import random
import string

def gen_action_k():
    return random.choice(string.ascii_letters)

def gen_rw():
    a = ''.join(random.choices(string.ascii_uppercase + string.digits, k=10))
    return a

#def gen_list_m():


def gen_list_k(dur_h, start_h, start_m, start_s, date):
    secs = start_s
    mins = start_m
    hours = start_h
    secs_a = 0 #start_s
    mins_a = 0 #start_m
    hours_a = 0 #start_h
    out = []
    while (hours_a*60*60 + mins_a*60 + secs_a) < dur_h*60:
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
        secs_a += 1
        if secs_a == 60:
            secs_a = 0
            mins_a += 1
            if mins_a == 60:
                mins_a = 0
                hours_a += 1
                if hours_a == 24:
                    print("pisos") 
    return out

def gen_list_rw(dur_h, start_h, start_m, start_s, date):
    secs = start_s
    mins = start_m
    hours = start_h
    secs_a = 0 #start_s
    mins_a = 0 #start_m
    hours_a = 0 #start_h
    out = []
    while (hours_a*60*60 + mins_a*60 + secs_a) < dur_h*60*60:
        node = [date, hours, mins, secs, gen_rw()]
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
        secs_a += 1
        if secs_a == 60:
            secs_a = 0
            mins_a += 1
            if mins_a == 60:
                mins_a = 0
                hours_a += 1
                if hours_a == 24:
                    print("pisos") 
    return out

def print_doc(name, out):
    f = open(name, "w")
    for i in out:
        d = i[0]
        if i[1]<10:
            h = '0' + str(i[1])
        else:
            h = str(i[1])
        if i[2]<10:
            m = '0' + str(i[2])
        else:
            m = str(i[2])
        if i[3]<10:
            s = '0' + str(i[3])
        else:
            s = str(i[3])
        #a = ''
        a = i[4]
        line = d + ' ' + h + ':' + m + ':' + s + ' ' + a + '\n'
        f.write(line)
    f.close

print_doc('test10h.txt', gen_list_rw(10, 12,30,00,'06/02/2021'))

