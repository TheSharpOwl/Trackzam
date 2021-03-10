FROM python:3
ENV PYTHONUNBUFFERED=1
WORKDIR /code
COPY req.txt /code/
RUN pip install -r req.txt
COPY . /code/
RUN echo "from django.contrib.auth import get_user_model; User = get_user_model(); User.objects.create_superuser('admin1', 'admin1@myproject.com', 'admin1')" | python manage.py shell
