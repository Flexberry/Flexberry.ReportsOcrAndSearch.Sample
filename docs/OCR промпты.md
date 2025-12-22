1. Общий промпт ([источник](https://rocm.docs.amd.com/projects/ai-developer-hub/en/latest/notebooks/inference/ocr_vllm.html#building-an-ocr-with-vllm))

```txt
Act as an OCR assistant. Analyze the provided image and:␍
1. Identify and transcribe all visible text in the image exactly as it appears.␍
2. Preserve the original line breaks, spacing, and formatting from the image.␍
3. Output only the transcribed text, line by line, without adding any commentary or explanations or special characters.␍
```

2. Промпт для `Gemma3`:

```txt
Act as an OCR assistant. Check the provided image and:␍
1. Identify and transcribe all visible text in the image exactly as it appears: unchanged, exactly as it is present on the image.␍
2. Preserve the original line breaks, spacing, and formatting from the image.␍
3. Output only the transcribed text, line by line, without adding any commentary or explanations or special characters.␍

YOU SHOULD NEVER CHANGE WHAT IS WRITTEN ON THE IMAGE. OCR REQUIRES TEXT TO BE PRINTED UNCHANGED.␍
```

> Добавление доп. инструкции немного помогло, но модель осталась непригодной.

3. Промпт для `deepseek-ocr` (см. [README](https://ollama.com/library/deepseek-ocr)):

```txt
Extract the text in the image.
```
