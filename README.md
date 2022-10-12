# mongo-dotnet-labs

Restore sample data to mongodb
```shell
curl https://atlas-education.s3.amazonaws.com/sampledata.archive -o sampledata.archive

docker cp sampledata.archive <container_name>:/sampledata.archive

docker exec -i <container_name> /usr/bin/mongorestore --username <username> --password <password> --authenticationDatabase admin --archive=sampledata.archive
```

