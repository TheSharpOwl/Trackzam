from django.conf.urls import url
from API import views

urlpatterns = [
    url(r'^api/send_audio_logs$', views.not_implemented),
    url(r'^api/send_keyboard_logs$', views.not_implemented),
    url(r'^api/send_windows_logs$', views.not_implemented),
    url(r'^api/send_mouse_logs$', views.not_implemented),
    url(r'^api/send_pics$', views.not_implemented),
    url(r'^api/read_all_users$', views.not_implemented),
    url(r'^api/read_user_metadata/(?P<user_id>[0-9]+)$', views.not_implemented),
    url(r'^api/read_user_data/(?P<user_id>[0-9]+)/(?P<session_id>[0-9]+)$', views.not_implemented),
]