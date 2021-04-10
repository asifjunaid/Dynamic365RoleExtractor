using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic_365_Role_Extractor.Utilities
{
    public class SpreadSheetCreator
    {
        public void CreateSpreadsheetWorkbook(string filePath, DataTable dt)
        {
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.
                Create(filePath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            WorkbookStylesPart workStylePart = workbookpart.AddNewPart<WorkbookStylesPart>();
            workStylePart.Stylesheet = GetStyleSheet();

            // Add Sheets to the Workbook.
            Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Roles"
            };
            sheets.Append(sheet);

            workbookpart.Workbook.Save();

            // Close the document.
            spreadsheetDocument.Close();



            using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(filePath, true))
            {
                WorksheetPart worksheetPart_ = spreadSheet.WorkbookPart.WorksheetParts.First();
                uint rowCounter = 1;
                foreach (DataRow row in dt.Rows)
                {
                    int d = 0;
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        d = c + 1;
                        Cell cell = InsertCellInWorksheet(GetColumnName(d), rowCounter, worksheetPart_);
                        // Set the value of cell A1.
                        //cell.CellValue = new CellValue(row[c].ToString().Replace("<b>","").Replace("<m>", ""));
                        //cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
                        cell.InlineString = new InlineString() { Text = new Text(row[c].ToString().Replace("<b>", "").Replace("<m>", "")) };
                        cell.DataType = CellValues.InlineString;
                        
                        if (row[c].ToString().IndexOf("<b>")>-1) {
                            AddBold(spreadSheet, cell);
                        }
                        if (row[c].ToString().IndexOf("<m>") > -1){
                            MergeTwoCells(worksheetPart_.Worksheet, 
                                (GetColumnName(d)+ rowCounter), 
                                (GetColumnName(d+1)+ rowCounter));
                        }                        
                    }
                    rowCounter++;
                }                
                worksheetPart_.Worksheet.Save();
            }

        }
        public  void CreateSpreadsheetWorkbook(string filepath)
        {
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.
                Create(filepath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Add Sheets to the Workbook.
            Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Roles"
            };
            sheets.Append(sheet);

            workbookpart.Workbook.Save();

            // Close the document.
            spreadsheetDocument.Close();
        }


        // Given a column name, a row index, and a WorksheetPart, inserts a cell into the worksheet. 
        // If the cell already exists, returns it. 
        private Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;
            
            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // If there is not a cell with the specified column name, insert one.  
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
            {
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            }
            else
            {
                // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>())
                {
                    if (cell.CellReference.Value.Length == cellReference.Length)
                    {
                        if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                        {
                            refCell = cell;
                            break;
                        }
                    }
                }

                Cell newCell = new Cell() { CellReference = cellReference };
                row.InsertBefore(newCell, refCell);
                
                //worksheet.Save();
                return newCell;
            }
        }

        void AddBold(SpreadsheetDocument document, Cell c)
        {
            Fonts fs = AddFont(document.WorkbookPart.WorkbookStylesPart.Stylesheet.Fonts);
            AddCellFormat(document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats, document.WorkbookPart.WorkbookStylesPart.Stylesheet.Fonts);
            c.StyleIndex = (UInt32)(document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats.Elements<CellFormat>().Count() - 1);            
        }
        Fonts AddFont(Fonts fs)
        {
            Font font2 = new Font();
            Bold bold1 = new Bold();
            FontSize fontSize2 = new FontSize() { Val = 11D };
            Color color2 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName2 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering2 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme2 = new FontScheme() { Val = FontSchemeValues.Minor };

            font2.Append(bold1);
            font2.Append(fontSize2);
            font2.Append(color2);
            font2.Append(fontName2);
            font2.Append(fontFamilyNumbering2);
            font2.Append(fontScheme2);

            fs.Append(font2);
            return fs;
        }
        void AddCellFormat(CellFormats cf, Fonts fs)
        {
            CellFormat cellFormat2 = new CellFormat() { NumberFormatId = 0, FontId = (UInt32)(fs.Elements<Font>().Count() - 1), FillId = 0, BorderId = 0, FormatId = 0, ApplyFill = true };
            cf.Append(cellFormat2);
        }

        public void MergeTwoCells(Worksheet worksheet,  string cell1Name, string cell2Name)
        {
            
                if (worksheet == null || string.IsNullOrEmpty(cell1Name) || string.IsNullOrEmpty(cell2Name))
                {
                    return;
                }

                MergeCells mergeCells;
                if (worksheet.Elements<MergeCells>().Count() > 0)
                {
                    mergeCells = worksheet.Elements<MergeCells>().First();
                }
                else
                {
                    mergeCells = new MergeCells();

                    // Insert a MergeCells object into the specified position.
                    if (worksheet.Elements<CustomSheetView>().Count() > 0)
                    {
                        worksheet.InsertAfter(mergeCells, worksheet.Elements<CustomSheetView>().First());
                    }
                    else if (worksheet.Elements<DataConsolidate>().Count() > 0)
                    {
                        worksheet.InsertAfter(mergeCells, worksheet.Elements<DataConsolidate>().First());
                    }
                    else if (worksheet.Elements<SortState>().Count() > 0)
                    {
                        worksheet.InsertAfter(mergeCells, worksheet.Elements<SortState>().First());
                    }
                    else if (worksheet.Elements<AutoFilter>().Count() > 0)
                    {
                        worksheet.InsertAfter(mergeCells, worksheet.Elements<AutoFilter>().First());
                    }
                    else if (worksheet.Elements<Scenarios>().Count() > 0)
                    {
                        worksheet.InsertAfter(mergeCells, worksheet.Elements<Scenarios>().First());
                    }
                    else if (worksheet.Elements<ProtectedRanges>().Count() > 0)
                    {
                        worksheet.InsertAfter(mergeCells, worksheet.Elements<ProtectedRanges>().First());
                    }
                    else if (worksheet.Elements<SheetProtection>().Count() > 0)
                    {
                        worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetProtection>().First());
                    }
                    else if (worksheet.Elements<SheetCalculationProperties>().Count() > 0)
                    {
                        worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetCalculationProperties>().First());
                    }
                    else
                    {
                        worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetData>().First());
                    }
                }

                // Create the merged cell and append it to the MergeCells collection.
                MergeCell mergeCell = new MergeCell() { Reference = new StringValue(cell1Name + ":" + cell2Name) };
                mergeCells.Append(mergeCell);

        }
        Stylesheet GetStyleSheet() {
            var stylesheet = new Stylesheet();

            // Default Font
            var fonts = new Fonts() { Count = 1, KnownFonts = BooleanValue.FromBoolean(true) };
            var font = new Font
            {
                FontSize = new FontSize() { Val = 11 },
                FontName = new FontName() { Val = "Calibri" },
                FontFamilyNumbering = new FontFamilyNumbering() { Val = 2 },
                FontScheme = new FontScheme() { Val = new EnumValue<FontSchemeValues>(FontSchemeValues.Minor) }
            };
            fonts.Append(font);
            stylesheet.Append(fonts);

            // Default Fill
            var fills = new Fills() { Count = 1 };
            var fill = new Fill();
            fill.PatternFill = new PatternFill() { PatternType = new EnumValue<PatternValues>(PatternValues.None) };
            fills.Append(fill);
            stylesheet.Append(fills);

            // Default Border
            var borders = new Borders() { Count = 1 };
            var border = new Border
            {
                LeftBorder = new LeftBorder(),
                RightBorder = new RightBorder(),
                TopBorder = new TopBorder(),
                BottomBorder = new BottomBorder(),
                DiagonalBorder = new DiagonalBorder()
            };
            borders.Append(border);
            stylesheet.Append(borders);

            // Default cell format and a date cell format
            var cellFormats = new CellFormats() { Count = 2 };

            var cellFormatDefault = new CellFormat { NumberFormatId = 0, FormatId = 0, FontId = 0, BorderId = 0, FillId = 0 };
            cellFormats.Append(cellFormatDefault);

            var cellFormatDate = new CellFormat { NumberFormatId = 22, FormatId = 0, FontId = 0, BorderId = 0, FillId = 0, ApplyNumberFormat = BooleanValue.FromBoolean(true) };
            cellFormats.Append(cellFormatDate);

            stylesheet.Append(cellFormats);

            return stylesheet;
        }
        public string GetColumnName(int column)
        {
            string columnString = "";
            decimal columnNumber = column;
            while (columnNumber > 0)
            {
                decimal currentLetterNumber = (columnNumber - 1) % 26;
                char currentLetter = (char)(currentLetterNumber + 65);
                columnString = currentLetter + columnString;
                columnNumber = (columnNumber - (currentLetterNumber + 1)) / 26;
            }
            return columnString;
        }
    }
}
