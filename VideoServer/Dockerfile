FROM python:3
ENV PYTHONUNBUFFERED=1
WORKDIR /code
COPY req.txt /code/
RUN apt-get update ##[edited]
RUN apt-get install ffmpeg libsm6 libxext6  -y
RUN bash -c "pip install cmake && pip install -r req.txt"
COPY . /code/
RUN echo "from django.contrib.auth import get_user_model; User = get_user_model(); User.objects.create_superuser('admin', 'admin@myproject.com', 'admin')" | python manage.py shell
# RUN echo "from django.contrib.auth import get_user_model; User = get_user_model(); User.objects.create_user('John', 'John@myproject.com', 'johnpassword')" | python manage.py shell

