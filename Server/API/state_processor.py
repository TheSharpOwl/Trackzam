import datetime

def parse_into_list(file):
    log = []
    for byte_line in file:
        line = byte_line

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

def generate(time1, time2, log_m, log_k, log_a, log_v, log_w):
    sus_v = gen_states_for_video(time1,time2, log_v)
    sus_a = gen_states_for_audio(time1,time2, log_a)
    sus_w = gen_states_for_window(time1,time2, log_w)
    delta = datetime.timedelta(seconds=1)
    time_temp = time1
    k_v = 0
    k_a = 0
    k_w = 0
    k_v_old = 0
    k_a_old = 0
    k_w_old = 0
    amogus = []
    while time_temp < time2 :

        if k_w < len(sus_w):
            date = sus_w[k_w]['date'].split('-')
            time = sus_w[k_w]['time'].split(':')
            timestamp1 = datetime.datetime(int(date[0]),int(date[1]),int(date[2]),int(time[0]),int(time[1]),int(time[2]))
            if timestamp1 == time_temp:
                amogus.append(sus_w[k_w])
                k_w += 1

        if k_v < len(sus_v):
            date = sus_v[k_v]['date'].split('-')
            time = sus_v[k_v]['time'].split(':')
            timestamp2 = datetime.datetime(int(date[0]),int(date[1]),int(date[2]),int(time[0]),int(time[1]),int(time[2]))
            if timestamp2 == time_temp:
                amogus.append(sus_v[k_v])
                k_v += 1
            
            

        if k_a < len(sus_a):
            date = sus_a[k_a]['date'].split('-')
            time = sus_a[k_a]['time'].split(':')
            timestamp3 = datetime.datetime(int(date[0]),int(date[1]),int(date[2]),int(time[0]),int(time[1]),int(time[2]))
            if timestamp3 == time_temp:
                amogus.append(sus_a[k_a])
                k_a += 1

        #if time_temp < timestamp1 and time_temp < timestamp2 and time_temp < timestamp3:
        
        time_temp += delta
    


    return amogus

def gen_states_for_audio(time1, time2, log:list): 
    out = []
    for i in log:
        date = i['date'].split('-')
        time = i['time'].split(':')
        timestamp = datetime.datetime(int(date[0]),int(date[1]),int(date[2]),int(time[0]),int(time[1]),int(time[2]))
        if timestamp >= time1 and timestamp <= time2:
            state_temp = i['state'].split(',')
            if len(state_temp) != 1:
                state = float(state_temp[0] + '.' + state_temp[1])
            else:
                state = int(state_temp[0])
            if float(state) > 0.1:
                i['state'] = 'Loud_noise'
                out.append(i) 
    print(out)
    print('\n')
    return out

def gen_states_for_video(time1, time2, log:list): 
    out = []
    for i in log:
        date = i['date'].split('-')
        time = i['time'].split(':')
        timestamp = datetime.datetime(int(date[0]),int(date[1]),int(date[2]),int(time[0]),int(time[1]),int(time[2]))
        if timestamp >= time1 and timestamp <= time2:
            if i['state']!='Present':
                out.append(i) 
    print(out)
    print('\n')
    return out

def gen_states_for_window(time1, time2, log_w:list): 
    out = []
    for i in log_w:
        date = i['date'].split('-')
        time = i['time'].split(':')
        timestamp = datetime.datetime(int(date[0]),int(date[1]),int(date[2]),int(time[0]),int(time[1]),int(time[2]))
        if timestamp >= time1 and timestamp <= time2:
            i['state'] = "Window_Focus_Changed_To_" + i['state']
            out.append(i) 
    print(out)
    print('\n')
    return out

#f_w = open('C:/Users/Jameel/Desktop/Trackzam/Server/Test/window.txt', 'r')
#f_a = open('C:/Users/Jameel/Desktop/Trackzam/Server/Test/audio.txt', 'r')
#f_v = open('C:/Users/Jameel/Desktop/Trackzam/Server/Test/video.txt', 'r')
#log_w = parse_into_list(f_w)
#log_a = parse_into_list(f_a)
#log_v = parse_into_list(f_v)
#log_m = parse_into_list(f_v)
#log_k = parse_into_list(f_v)

#t1 = datetime.datetime(2020,12,27,12,45,13)
#t2 = datetime.datetime(2020,12,27,12,45,34)
#log1 = generate(t1,t2,log_m, log_k, log_a, log_v, log_w)
#t3 = t2 - t1

#for i in log1:
#    print(i)