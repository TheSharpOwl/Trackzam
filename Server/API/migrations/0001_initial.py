# Generated by Django 3.1.6 on 2021-02-06 18:05

from django.db import migrations, models


class Migration(migrations.Migration):

    initial = True

    dependencies = [
    ]

    operations = [
        migrations.CreateModel(
            name='AudioRecord',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('user_id', models.IntegerField(default=0)),
                ('data', models.DateField()),
                ('time', models.TimeField()),
                ('state', models.CharField(default='', max_length=100)),
            ],
        ),
    ]