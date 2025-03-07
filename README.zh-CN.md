# REPO MOD 安装器

[English](./README.md)

## 如何使用

打开 REPO MOD 安装器后：

1. 选择要安装的MOD，默认已自动选中第一个。
2. 点击“安装MOD”，即可自动安装。
3. 再点击“开始游戏”，即可打开游戏。

如果打不开，复制下面这个地址用浏览器下载，下载安装.NET运行时：
https://dotnet.microsoft.com/zh-cn/download/dotnet-framework/thank-you/net48-offline-installer

## 如何制作安装包

1. 在应用程序文件夹下的“Assets”子文件夹中创建你的MOD包文件夹，例如“Assets/自定义MOD包”。
2. 在刚刚创建的“Assets/自定义MOD包”文件夹下创建“Base”文件夹，将你的MOD基础包放入其中（确保该层目录包含BepInEx文件夹），安装时将会全部复制到游戏根目录。
3. （可选）在“Assets/自定义MOD包”文件夹下创建“Optional”文件夹，该文件夹为可选安装包，其中每个子文件夹为一个可选安装包，文件结构与基础包文件夹相同。
4. （可选）在“Assets/自定义MOD包”文件夹下创建“README.txt”文件，编辑添加要在安装界面显示给用户的文字，保存。
5. （可选）在“Assets/自定义MOD包”文件夹下创建“ABOUT.txt”文件，编辑输入你的个人首页网址（需要为 http:// 或 https:// 开头），保存。
6. （可选）在“Assets/自定义MOD包”文件夹下创建“UPDATE.txt”文件，编辑输入MOD包更新网址（需要为 http:// 或 https:// 开头），保存。
7. 打开“REPOModHelperArchiver.exe”，保持“自定义基础MOD文件夹”复选框不勾选，点击“开始打包”按钮，将会生成一个“installer.exe”的一键安装包。
8. 玩家双击打开该“installer.exe”一键安装包，即可一键安装。

## 截图

![image](https://github.com/tinymins/LethalCompanyModHelper/assets/1808990/e4d5c3c2-0665-4606-98ab-74147e6a4a6e)
![image](https://github.com/tinymins/LethalCompanyModHelper/assets/1808990/982ade43-e6c8-494a-bb90-2ce58cc6cdc5)
![image](https://github.com/tinymins/LethalCompanyModHelper/assets/1808990/022ee961-6b9b-488d-9d1d-20cd422a396e)
