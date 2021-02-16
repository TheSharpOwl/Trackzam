from django.conf.urls import url
from API import views

urlpatterns = [
    url(r'^api/send_zip$', views.send_zip),
]