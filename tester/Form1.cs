﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tester.设计模式.行为型.策略模式;
using tester.设计模式.行为型.观察者模式;
using tester.重构案例.SwitchToStrategy.After;
using tester.重构案例.SwitchToStrategy.Before;
using Zzh.Lib.DB.Context;
using Zzh.Model.DB;

namespace tester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        List<Sys_Areas> areaList = new List<Sys_Areas>();
        private void Form1_Load(object sender, EventArgs e)
        {
            ClientCodeBefore before = new ClientCodeBefore();
            var p1 = before.GetPrice(重构案例.SwitchToStrategy.Before.State.NewYork);
            ClientCodeAfter after = new ClientCodeAfter();
            var p2 = after.GetPrice(重构案例.SwitchToStrategy.After.State.NewYork);
            //using (NLiteDBContex visiter = new NLiteDBContex())
            //{
            //    areaList = visiter.Sys_Areas.ToList();
            //    BindTreeView(null, 0);
            //}
        }
        private void BindTreeView(TreeNode node, int parentid = 0)
        {
            var list = areaList.Where(p => p.parentid == parentid).ToList();
            if (list.Count() > 0)
            {
                foreach (Sys_Areas area in list)
                {
                    if (area.parentid == 0)
                    {
                        //添加根节点
                        TreeNode nodePara = new TreeNode();
                        nodePara.Text = area.areaname;
                        nodePara.Tag = area.id;
                        this.treeView1.Nodes.Add(nodePara);
                        BindTreeView(nodePara, area.id);
                    }
                    else
                    {
                        TreeNode nodeChild = new TreeNode();
                        nodeChild.Text = area.areaname;
                        nodeChild.Tag = area.id;
                        node.Nodes.Add(nodeChild);
                        BindTreeView(nodeChild, area.id);
                    }
                }
            }
        }
        static void InitializeEQueue()
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //主要解决：在有多种算法相似的情况下，使用 if...else 所带来的复杂和难以维护。
            StrategyContext contex = new StrategyContext(new OperationMultiply());
            var res = contex.executeStrategy(10, 2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //通常用于一对多修改时，修改表头时，自动修改表体值
            Subject subject = new Subject();
            new BinaryObserver(subject);
            new OctalObserver(subject);
            subject.setState(15);
            subject.setState(10);
        }
    }
}
