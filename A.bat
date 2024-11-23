for /R %%i  in (*.gd) do python3 gd2cs.py -i %%i  -o %%~pi%%~ni.cs
