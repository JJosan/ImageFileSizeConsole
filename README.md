<h1>Image File Size Console</h1>
<h2>Usage</h2>
./FileDeleter.exe -p <strong>folderPath</strong> -d <strong>date</strong> -m <strong>maxWidth</strong> (optional)
<br />
./ImageFileSizeConsole.exe -p "C:\Users\jason\Desktop\folder" -d 3
<br />
./ImageFileSizeConsole.exe -p "C:\Users\jason\Desktop\folder" -d 3 -m 1000

<h2>Example Flags</h2>
<h3>folderPath</h3>
-p {Absolute Path}
<br />
-p "C:\users\jason\Desktop\folder"
<h3>date</h3>
-d {n days ago OR date in mm/dd/yyyy format}
<br />
-d 7
<br />
-d 7/29/2022
<br />
<h3>maxWidth (optional)</h3> 
-m {maximum width of reduced image in pixels}
<br />
-m 1000