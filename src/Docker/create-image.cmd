docker build --no-cache -f SQL\Dockerfile.PostgreSql -t reportsocrandsearch/postgre-sql ../SQL

docker build --no-cache -f Dockerfile -t reportsocrandsearch/app ../..

docker build --no-cache -f OCR\Dockerfile.Tesseract -t reportsocrandsearch/ocr-service-tesseract .

docker build --no-cache -f ElasticSearch\Dockerfile.ElasticSearch -t reportsocrandsearch/elasticsearch .
