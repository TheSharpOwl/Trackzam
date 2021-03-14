# use django shell and run inside it

from django.contrib.auth.models import User
user = User.objects.create_superuser('admin2', 'admin2@thebeatles.com', 'admin')
user.save()