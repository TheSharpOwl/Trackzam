from django.core.management.base import BaseCommand
from django.contrib.auth import get_user_model
import os

class Command(BaseCommand):

    def handle(self, *args, **options):

        DJANGO_U_NAME = os.environ.get('DJANGO_U_NAME')
        DJANGO_U_EMAIL = os.environ.get('DJANGO_U_EMAIL')
        DJANGO_U_PASSWORD = os.environ.get('DJANGO_U_PASSWORD')

        User = get_user_model()
        if not User.objects.filter(username=DJANGO_U_NAME).exists():
            user = User.objects.create_user(
                username=DJANGO_U_NAME,
                email=DJANGO_U_EMAIL,
                password=DJANGO_U_PASSWORD)
            print('Creating account for %s (%s)' % (DJANGO_U_NAME, DJANGO_U_EMAIL))

            user.save()

        else:
            print('User with that name '+ DJANGO_U_NAME +' already exists')