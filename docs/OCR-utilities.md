# OCR-utilities
В данном Sample-репозитории рассмотрены две наиболее популярные OpenSource утилиты - Tesseract и PaddleOCR. Используется Tesseract, а PaddleOCR представлен только Docker-файлом.

## Tesseract

OpenSource утилита от Google. Изначально была разработана компанией Hewlett-Packard. Распространяется по лицензии Apache 2.0. В основе лежит движок libtesseract.

Особенности:

- Распознает текст в файлах изображений (соотв принимает только их).
- Поддерживает распознавание символов UTF-8.
- Поддерживает распознавание текстов более чем на 100 языках.
- Выводит результат распознавания в файлах .txt, hOCR (HTML), PDF, invisible-text-only PDF, TSV, ALTO.
- Простой запуск в Docker.

Недостатки

- Для более хорошего результата требует дополнительной обработки изображений и тренировки модели.
- Не принимает PDF.

[Официальный репозиторий](https://github.com/tesseract-ocr/tesseract) 

**Dockerfile**

```dockerfile
FROM ubuntu:18.04

RUN apt-get update \
 && apt-get -y install tesseract-ocr \
 && apt-get -y install tesseract-ocr-all \
 && apt-get -y install imagemagick

# disable imagemagic policy for pdf-png convert.
COPY /OCR/policy.xml /etc/ImageMagick-6/policy.xml
```

В файле дополнительно устанавливается утилита imagemagick для преобразования pdf в png.

**Запуск в docker**

1) Запустить контейнер

docker run -it -v c:/tmp/app:/home/work reportsocrandsearch/ocr-service-tesseract

2) Преобразовать pdf в png (в высоком качестве)

convert -density 300 -trim 2.pdf -quality 100 2.png

3) Распознать текст и вывести результат в файлах

tesseract 1.png 1_result -l rus+eng --psm 1 --oem 3 txt pdf hocr

Создаст три файла txt pdf hocr с результатом. Распознавать будет два языка - русский и английский.

## PaddleOCR

Разработка китайской компании Baidu.

Особенности:

- Распознает текст в файлах изображений.
- Поддерживает распознавание текстов на разных языках.
- Больше подходит для распознавания неструктурированных изображений - не документов, этикетки, рекламные щиты и т.д.
- В официальном репозитории есть утилита, которая предоставляет графический интерфейс.

Недостатки:

- Более трудно запустить. Нужно устанавливать много дополнительных зависимостей.
- Выводит результат только в виде массива строк. В каждой из которых содержится информации о координате на исходном изображении и области, в которой был обнаружен и распознан текст. Ну и сам текст. Обычно в одной строке выводится обычно одно слово.
- Меньше информации в интернете.

[Официальный репозиторий](https://github.com/PaddlePaddle/PaddleOCR)

**Dockerfile**

```dockerfile
FROM ubuntu:20.04

RUN apt-get update \
 && apt-get install -y software-properties-common \
 && add-apt-repository ppa:deadsnakes/ppa \
 && apt-get install -y python3.8 \
 && apt-get install -y python3-pip \
 && apt-get install -y python3-opencv \
 && apt-get install -y python3-pyqt5

RUN python3 -m pip install paddlepaddle==0.0.0 -f https://www.paddlepaddle.org.cn/whl/linux/cpu-mkl/develop.html \
 && pip install "paddleocr>=2.6.0" \
 && pip install opencv-python \
 && pip install --upgrade numpy
 
RUN mkdir /home/work
WORKDIR /home/work
```

**Запуск в docker**

1) Запустить контейнер.

docker run -it -v c:/tmp/app:/home/work reportsocrandsearch/ocr-service

2) Распознать текст.

paddleocr --image_dir 1.png --lang=ru