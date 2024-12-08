from lanzou.api import LanZouCloud

lz = LanZouCloud()

username = "17527335328"
password = "DjUCd3EC"

file_path = '.code\ChmlFrp_WPF_Clienter\bin\Release\ChmlFrp_WPF_Clienter.exe'
folder_path = '/ChmlFrp_WPF_Clienter'

upload_response = lz.upload_file(file_path, folder_path)