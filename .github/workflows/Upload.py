from lanzou.api import LanZouCloud

lz = LanZouCloud()
lz.login("17527335328", "DjUCd3EC")

file_path = r".code\ChmlFrp_WPF_Clienter\bin\Release\ChmlFrp_WPF_Clienter.exe"
folder_path = '/ChmlFrp_WPF_Clienter'
upload_response = lz.upload_file(file_path, folder_path)

if upload_response['code'] == 0:
    print("文件上传成功！")
    print("分享链接:", upload_response['link'])
else:
    print("文件上传失败:", upload_response['message'])