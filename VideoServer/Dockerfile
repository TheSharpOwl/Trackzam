FROM python:3
ENV PYTHONUNBUFFERED=1
WORKDIR /code
COPY req.txt /code/
RUN apt-get update ##[edited]
RUN apt-get install ffmpeg libsm6 libxext6  -y
RUN bash -c "pip3 install cmake && pip3 install -r req.txt"
COPY . /code/