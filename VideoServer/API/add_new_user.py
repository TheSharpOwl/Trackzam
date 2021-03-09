# use django shell and run inside it

from django.contrib.auth.models import User
user = User.objects.create_user('John', 'lennon@thebeatles.com', 'johnpassword')
user.last_name = 'Lennon'
user.save()