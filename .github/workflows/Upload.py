import os
from lanzou.api import LanZouCloud

# Initialize LanZouCloud instance
lz = LanZouCloud()

# Login credentials
username = "17527335328"
password = "DjUCd3EC"

try:
    lz.login(username, password)
except Exception as e:
    print(f"登录失败: {e}")
    exit(1)

# Check if logged in successfully
if not lz.is_logged_in():
    print("未成功登录，无法上传文件。")
    exit(1)

file_path = '.code\ChmlFrp_WPF_Clienter\bin\Release\ChmlFrp_WPF_Clienter.exe'
folder_path = '/ChmlFrp_WPF_Clienter'

if os.path.isfile(file_path):
    try:
        # Attempt to get the folder's file list
        # Ensure that the folder_id is fetched correctly
        folder_id = lz.get_folder_id(folder_path)  # Update this method based on your implementation
        
        # Check what folder_id looks like
        print(f"Folder ID: {folder_id}")  # Debugging Line

        # Now upload the file
        upload_response = lz.upload_file(file_path, folder_id)

        # Output the full upload response for debugging
        print("Upload Response:", upload_response)  # Debugging Line

        if upload_response['code'] == 0:
            print("文件上传成功！")
            print("分享链接:", upload_response['link'])
        else:
            print("文件上传失败:", upload_response['message'])
    except Exception as e:
        print(f"上传过程中发生错误: {e}")
else:
    print("文件未找到:", file_path)
