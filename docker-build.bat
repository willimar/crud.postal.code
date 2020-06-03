docker build -t postalcode-api .

heroku login
heroku container:login

docker tag postalcode-api registry.heroku.com/postalcode-api/web
docker push registry.heroku.com/postalcode-api/web

heroku container:release web -a postalcode-api

heroku logs --tail --app postalcode-api