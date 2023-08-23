namespace IIS.ReportsOcrAndSearch.OcrService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// ����������, ������������� ������� �� ������������� ������.
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OcrRecognizer : ControllerBase
    {
        /// <summary>
        /// �����, �������������� Post-������ �� ������������� ���������� pdf �����. ����� ������������� ���� ������������ � ElasticSearch.
        /// ������ ������� - http://localhost:6600/api/OcrRecognizer/RunRecognizeUploadedPdf?uploadDirectory=...&uploadKey=...&fileName=...
        /// </summary>
        /// <param name="uploadDirectory">���������� � ������������ pdf �������.</param>
        /// <param name="uploadKey">GUID ��������.</param>
        /// <param name="fileName">��� �����.</param>
        [HttpPost]
        public IActionResult RunRecognizeUploadedPdf(string uploadDirectory, string uploadKey, string fileName)
        {

            if (!System.IO.File.Exists(Path.Combine(uploadDirectory, uploadKey, fileName)))
            {
                return BadRequest("Recognition error: File not found");
            }

            string fileDirectory = Path.Combine(uploadDirectory, uploadKey);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
            string pngFolder = "png";
            string recognitionFolder = "recognition";

            string pngDirectory = Path.Combine(fileDirectory, pngFolder);
            string recognitionDirectory = Path.Combine(fileDirectory, recognitionFolder);

            try
            {
                CreatePngFilesFromPdf(fileDirectory, pngDirectory, fileNameWithoutExt);
                RecognizePng(pngDirectory, recognitionDirectory);
                MergeTxt(recognitionDirectory, fileNameWithoutExt);
            }
            catch (Exception ex)
            {
                return BadRequest("Recognition error: " + ex.Message);
            }

            // ��������� ������������ � ��������� fileNameWithoutExt .txt � Elastic.

            return Ok("Recognition completed");
        }

        /// <summary>
        /// ������������ ��������� pdf � ����� png ������.
        /// </summary>
        /// <param name="mainDirectory">����������, ��� ��������� ����������� ����.</param>
        /// <param name="pngDirectory">���������� � ������� ����� ������������ png �����.</param>
        /// <param name="originFileNameWithoutExt">��� ������������ pdf �����, ��� ����������.</param>
        private void CreatePngFilesFromPdf(string mainDirectory, string pngDirectory, string originFileNameWithoutExt)
        {
            string pdfFile = Path.Combine(mainDirectory, originFileNameWithoutExt + ".pdf");
            string pngFile = Path.Combine(pngDirectory, originFileNameWithoutExt + ".png");

            var convertPdfToPngProcess = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"mkdir -p {pngDirectory} && convert -density 300 -trim {pdfFile} -quality 100 {pngFile}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            convertPdfToPngProcess.Start();

            string errors = convertPdfToPngProcess.StandardError.ReadToEnd();

            convertPdfToPngProcess.WaitForExit();

            if (convertPdfToPngProcess.ExitCode != 0)
            {
                throw new Exception(errors);
            }
        }

        /// <summary>
        /// ���������� png ����� � ��������� ��������� � txt.
        /// </summary>
        /// <param name="pngDirectory">���������� � png �������.</param>
        /// <param name="recognitionDirectory">���������� ��� ���������� ������������ ������.</param>
        private void RecognizePng(string pngDirectory, string recognitionDirectory)
        {
            // �������� ������ ���������������� png ������.
            List<string> pngFiles = Directory.GetFiles(pngDirectory).ToList();

            foreach (string pngFile in pngFiles)
            {
                string pngFileName = Path.GetFileNameWithoutExtension(pngFile);
                string resultFile = Path.Combine(recognitionDirectory, pngFileName + "_result");

                var convertPdfToPngProcess = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = $"-c \"mkdir -p {recognitionDirectory} && tesseract {pngFile} {resultFile} -l rus+eng --psm 1 --oem 3 txt\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };

                convertPdfToPngProcess.Start();

                string errors = convertPdfToPngProcess.StandardError.ReadToEnd();

                convertPdfToPngProcess.WaitForExit();

                if (convertPdfToPngProcess.ExitCode != 0)
                {
                    throw new Exception(errors);
                }
            }
        }

        /// <summary>
        /// ������� ��������� txt ������, ����������� � ������ pdf ��������� � ����.
        /// </summary>
        /// <param name="recognitionDirectory">���������� ��� ���������� ������������ ������.</param>
        /// <param name="fileNameWithoutExt">��� �����, ��� ������� ����� ����������� ���������.</param>
        private void MergeTxt(string recognitionDirectory, string fileNameWithoutExt)
        {
            string resultFile = Path.Combine(recognitionDirectory, fileNameWithoutExt + ".txt");

            if (System.IO.File.Exists(resultFile))
            {
                System.IO.File.Delete(resultFile);
            }

            // �������� ������ ������������ ������.
            List<string> txtFiles = Directory.GetFiles(recognitionDirectory, "*.txt").ToList();
            txtFiles.Sort();

            if (txtFiles.Count == 0)
            {
                throw new Exception("Merge txt error. Recognized files not found");
            }

            using (FileStream output = System.IO.File.Create(resultFile))
            {
                foreach (string existingFile in txtFiles)
                {
                    using (FileStream input = System.IO.File.OpenRead(existingFile))
                    {
                        input.CopyTo(output);
                    }
                }
            }
        }
    }
}