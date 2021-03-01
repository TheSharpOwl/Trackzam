import face_recognition
from PIL import Image
import cv2

def recognize():
    image = face_recognition.load_image_file("test.png")

    face_locations = face_recognition.face_locations(image)
    k=0
    for face_location in face_locations:
        k+=1
     # Print the location of each face in this image
        top, right, bottom, left = face_location
        print("A face is located at pixel location Top: {}, Left: {}, Bottom: {}, Right: {}".format(top, left, bottom, right))

      # You can access the actual face itself like this:
        face_arr = image[top:bottom, left:right]
        pil_image = Image.fromarray(face_arr)
        pil_image.save("img" + str(k) + ".png")

    print(face_locations)

def get_state(name):
    image = face_recognition.load_image_file(name)
    face_locations = face_recognition.face_locations(image)
    
    if len(face_locations) == 0:
        state = 0
    else:
        state = 1
    
    return state

def split_video(name):
    vidcap = cv2.VideoCapture(name)
    success,image = vidcap.read()
    count = 0
    while success:
        cv2.imwrite("./frames/frame%d.jpg" % count, image)     # save frame as JPEG file      
        success,image = vidcap.read()
        print('Read a new frame: ', success)
        count += 1
    return count

def gen_states(name, amount):
    f = open(name, "w")
    for i in range(amount):
        line = str(i) + ' ' + str(get_state('./frames/frame%d.jpg' % i)) + '\n'
        f.write(line)
    f.close


amount = split_video('test.mp4')
gen_states("test.txt", amount)

