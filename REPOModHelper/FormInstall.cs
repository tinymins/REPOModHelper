﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace REPOModHelper
{
    public partial class FormInstall : Form
    {
        private readonly List<string> deleteDirectories = new List<string> { "_state", "BepInEx" };
        private string gamePath = "";
        private string modPath = "";
        private List<string> optionalPaths = new List<string>();

        public FormInstall(string gamePath, string modPath)
        {
            this.gamePath = gamePath;
            this.modPath = modPath;
            InitializeComponent();
        }

        private static void CopyDirectory(string sourceDir, string targetDir)
        {
            // Check if the target directory exists, if not, create it.
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            // Copy each file into the new directory.
            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(targetDir, fileName);
                File.Copy(file, destFile, true); // true to overwrite
            }

            // Copy each subdirectory using recursion.
            foreach (string dir in Directory.GetDirectories(sourceDir))
            {
                string dirName = Path.GetFileName(dir);
                string destDir = Path.Combine(targetDir, dirName);
                CopyDirectory(dir, destDir);
            }
        }

        private void FormInstall_Load(object sender, EventArgs e)
        {
            // Initialize i18n
            this.Text = Program.GetI18nString("FromInstall/FormTitle");
            lblSelectOptional.Text = Program.GetI18nString("FromInstall/PleaseSelectOptionalModPacks:");
            btnInstall.Text = Program.GetI18nString("FromInstall/StartInstall");

            // 获取可选包列表
            var optionalPath = Path.Combine(modPath, "Optional");

            optionalPaths.Clear();
            if (Directory.Exists(optionalPath))
            {
                var directories = Directory.GetDirectories(optionalPath);
                foreach (var directory in directories)
                {
                    if (Directory.Exists(Path.Combine(directory, "BepInEx")))
                    {
                        optionalPaths.Add(directory);
                    }
                }
            }

            clbOptional.Items.Clear();
            foreach (var p in optionalPaths)
            {
                string item = Path.GetFileName(p);
                clbOptional.Items.Add(item);
            }

            if (clbOptional.Items.Count == 0)
            {
                this.Hide();
                StartInstall();
            }
        }

        private void StartInstall()
        {
            // 获取名为 "REPO" 的进程是否存在
            Process[] processes = Process.GetProcessesByName("REPO");
            if (processes.Length > 0)
            {
                if (clbOptional.Items.Count == 0)
                {
                    this.Close();
                }
                MessageBox.Show(Program.GetI18nString("FormInstall/ProcessExistsError"), Program.GetI18nString("FormInstall/Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 开始安装MOD
            btnInstall.Enabled = false;
            foreach (var dirName in deleteDirectories)
            {
                var dirPath = Path.Combine(gamePath, dirName);
                if (Directory.Exists(dirPath))
                {
                    Directory.Delete(dirPath, true);
                }
            }
            CopyDirectory(Path.Combine(modPath, "Base"), gamePath);
            foreach (var item in clbOptional.CheckedItems)
            {
                string optional = Path.Combine(modPath, "Optional", item.ToString());
                CopyDirectory(optional, gamePath);
            }
            btnInstall.Enabled = true;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Program.GetI18nString("FormInstall/REPOModPack")} \"{Path.GetFileName(modPath)}\" {Program.GetI18nString("FormInstall/InstallSuccess!")}");
            if (clbOptional.CheckedItems.Count > 0)
            {
                sb.AppendLine("");
                sb.AppendLine(Program.GetI18nString("FormInstall/InstalledOptionalModPacks:"));
                foreach (var item in clbOptional.CheckedItems)
                {
                    sb.AppendLine(item.ToString());
                }
            }
            MessageBox.Show(sb.ToString(), Program.GetI18nString("FormInstall/Success"), MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            StartInstall();
        }
    }
}
