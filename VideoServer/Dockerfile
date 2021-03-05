FROM python:3
ENV PYTHONUNBUFFERED=1
WORKDIR /code
COPY req.txt /code/
RUN bash -c "pip install cmake && pip install -r req.txt"
COPY . /code/
