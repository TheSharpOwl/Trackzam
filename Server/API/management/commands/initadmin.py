from django.core.management.base import BaseCommand
from django.contrib.auth import get_user_model
import os

class Command(BaseCommand):

    def handle(self, *args, **options):

        DJANGO_SU_NAME = os.environ.get('DJANGO_SU_NAME')
        DJANGO_SU_EMAIL = os.environ.get('DJANGO_SU_EMAIL')
        DJANGO_SU_PASSWORD = os.environ.get('DJANGO_SU_PASSWORD')

        User = get_user_model()
        if not User.objects.filter(username=DJANGO_SU_NAME).exists():
            superuser = User.objects.create_superuser(
                username=DJANGO_SU_NAME,
                email=DJANGO_SU_EMAIL,
                password=DJANGO_SU_PASSWORD)
            print('Creating account for %s (%s)' % (DJANGO_SU_NAME, DJANGO_SU_EMAIL))
            superuser.is_active = True
            superuser.is_admin = True
            superuser.save()

        else:
            print('Admin with name '+ DJANGO_SU_NAME +' already exists')
