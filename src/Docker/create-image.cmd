docker build --no-cache -f SQL\Dockerfile.PostgreSql -t reportsocrandsearch/postgre-sql ../SQL

docker build --no-cache -f Dockerfile -t reportsocrandsearch/app ../..
