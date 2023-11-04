redis-cli --cluster call 127.0.0.1:6379 MODULE LOAD redisearch.so OPT1 OPT2

redis-cli -a A1okgautam09@ MODULE LOAD /path/to/the/module.so

sudo cp /home/alokubuntu/redis/RedisJSON/bin/linux-x64-release/rejson.so /var/lib/redis/modules/.

loadmodule /var/lib/redis/modules/rejson.so

git clone --recursive https://github.com/RedisJSON/RedisJSON.git --branch v2.6.6


curl https://sh.rustup.rs -sSf | sh


Delete the redis - cli keys

redis-cli KEYS "user*" | xargs redis-cli DEL


https://developer.redis.com/create/windows/

https://administrator.de/en/ubuntu-build-redisearch-v2-module-for-redis-from-source-code-4707820878.html

https://stackoverflow.com/questions/40317106/failed-to-start-redis-service-unit-redis-server-service-is-masked

https://redis.io/docs/data-types/json/#command-line-option

https://github.com/RedisJSON/RedisJSON/issues/252

https://stackoverflow.com/questions/37436429/get-all-keys-from-redis-cache-database

https://forum.redis.com/t/how-to-get-all-created-indexes-names-in-nredisearch-c-client/163

https://redis.github.io/redis-om-dotnet/articles/aggregations/groups/groups.html

https://github.com/redis/redis-om-dotnet

https://dev.to/slorello/crud-with-redis-om-net-c-advent-4gif

https://redis.com/blog/redis-om-net/

https://dev.to/slorello/indexing-and-querying-embedded-objects-with-redis-om-net-3g4d