import face_recognition
from PIL import Image
import cv2
import os
import datetime

def recognize():
    image = face_recognition.load_image_file("")

    face_locations = face_recognition.face_locations(image)
    k=0
    for face_location in face_locations:
        k+=1
     # Print the location of each face in this image
        top, right, bottom, left = face_location
        #print("A face is located at pixel location Top: {}, Left: {}, Bottom: {}, Right: {}".format(top, left, bottom, right))

      # You can access the actual face itself like this:
        face_arr = image[top:bottom, left:right]
        pil_image = Image.fromarray(face_arr)
        pil_image.save("img" + str(k) + ".png")

    #print(face_locations)

def get_state(name):
    image = face_recognition.load_image_file(name)
    face_locations = face_recognition.face_locations(image)
    
    if len(face_locations) == 0:
        state = 0
    else:
        state = 1
    
    return state

def split_video(username, filename):
    video_path = "VideoServer/VideoFiles/"
    #print(filename)
    vidcap = cv2.VideoCapture(video_path+filename)
    success,image = vidcap.read()
    count = 0

    # "VideoServer/VideoAnalyser/john/"
    dir_of_user = "VideoServer/VideoAnalyser/"+username+"/"

    # "VideoServer/VideoAnalyser/john/4baa3c7fc0564c4d980bbb0a956ffe2a"
    dir_of_frames = "VideoServer/VideoAnalyser/"+username+"/"+filename.split('.')[0]

    try:
        access_rights = 0o755
        if not os.path.isdir(dir_of_user):
            os.mkdir(dir_of_user, access_rights)
        os.mkdir(dir_of_frames, access_rights)
    except OSError:
        pass
        #print ("Creation of the directory %s failed" % dir_of_frames)
    else:
        print ("Successfully created the directory %s" % dir_of_frames)

        while success:
            save_dir = dir_of_frames+"/frame%d.jpg" % count
            #print(save_dir)
            cv2.imwrite(save_dir, image)     # save frame as JPEG file
            success,image = vidcap.read()
            #print('Read a new frame: ', success)
            count += 1
    return count

def gen_states(username, filename, amount, stamp):
    dir_of_user = "VideoServer/LogFiles/"+ username
    loc_of_log_file = "VideoServer/LogFiles/"+username+"/"+filename
    access_rights = 0o777
    if not os.path.isdir(dir_of_user):
        os.makedirs(dir_of_user, access_rights, exist_ok=True)
    f = open(loc_of_log_file, "w")
    frames_dir = "VideoServer/VideoAnalyser/"+username+"/"+filename.split('.')[0]
    delta = datetime.timedelta(seconds=1)
    stamp_temp = stamp.split(' ')
    #print(stamp_temp)
    date_temp = stamp_temp[0].split('/')
    time_temp = stamp_temp[1].split(':')
    timestamp = datetime.datetime(int(date_temp[2]),int(date_temp[1]),int(date_temp[0]),
                                  int(time_temp[0]),int(time_temp[1]),int(time_temp[2]))
    for i in range(amount):
        timestamp = timestamp + delta
        current_frame_dir = frames_dir+"/frame%d.jpg" % i
        if get_state(current_frame_dir) == 0:
            line = timestamp.strftime("%d/%m/%Y %H:%M:%S") + ' Not_present' + '\n'
        else:
            line = timestamp.strftime("%d/%m/%Y %H:%M:%S") + ' Present' + '\n'
        f.write(line)
    f.close()


    return loc_of_log_file


