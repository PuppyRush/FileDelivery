using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.ComponentModel;
using System.Threading;
using System.Runtime.InteropServices;

namespace FileDelivery2_Client
{
    public class MyTreeView : TreeView
    {

        public List<string> nodeList;
        public List<String[]> rootFolderPath;
        public Dictionary<String, ShareFolder> shareFolderMap;
        private MainForm mainForm;
        public bool isInit;
        private TreeNode Candidate_Node;
        private TreeNode prevSelectedNode;
        private DirectoryInfo moveNodeDirInfo;
        private FileInfo moveNodeFileInfo;

        public delegate void Add(int i);

        public MyTreeView()
        {
            isInit = false;
            rootFolderPath = new List<string[]>();
            nodeList = new List<string>();

            m_SelectedNodes = new List<TreeNode>();
            base.SelectedNode = null;

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);

        }

        public void run()
        {
            if (!isInit)
            {
                mainForm.DoAddText("변수초기화가 되지 않았습니다");
                return;
            }

            this.Invoke(new Action(delegate()
            {


                //Nodes.Clear();

                DirectoryInfo di;
                int foldernum = shareFolderMap.Count;

                nodeList.Add(rootFolderPath.Count.ToString());
                for(int i=0 ; i < rootFolderPath.Count  ; i++){
                   
                    nodeList.Add(rootFolderPath[i][0]);
                    nodeList.Add(rootFolderPath[i][1]);
       
                }

                for (int i = 0; i < foldernum; i++)
                {


                    Nodes.Add(shareFolderMap.ElementAt(i).Value.pathname, shareFolderMap.ElementAt(i).Value.folderName);
                    di = new DirectoryInfo(shareFolderMap.ElementAt(i).Value.pathname);
  
                    //트리에 추가하기
                    if (di.Exists && di.GetDirectories().Length != 0)
                    {
                        AddDir(di, Nodes[i]);

                    }
                    else if (di.Exists)
                    {
                        FileInfo[] files = di.GetFiles();

                        
                        if (files.Length > 0)
                        {
                            nodeList.Add(files.Length.ToString());
                            nodeList.Add(di.FullName);
                            for (int l = 0; l < files.Length; l++)
                            {
                                nodeList.Add(files[l].FullName);
                                nodeList.Add(files[l].Name);
                                Nodes[Nodes.Count - 1].Nodes.Add(files[l].FullName, files[l].Name);

                            }

                        }
                    }
                }


            }));


            
        }

        public void AddNewNode(string nodeName)
        {


            DirectoryInfo di;
            ShareFolder sf = null;
            for (int i = 0; i < shareFolderMap.Count; i++)
                if (shareFolderMap.ElementAt(i).Value.folderName.Equals(nodeName))
                {
                    sf = shareFolderMap.ElementAt(i).Value;
                    break;
                }

            if (sf == null)
            {
                mainForm.DoAddText("새로운 공유폴더가 트리에 추가되지 않았습니다");
                return;
            }

            Invoke(new Action(delegate()
            {

                Nodes.Add(sf.pathname, sf.folderName);
                rootFolderPath.Add(new string[2]{sf.pathname, sf.folderName});

                di = new DirectoryInfo(sf.pathname);
                if (di.Exists && di.GetDirectories().Length != 0)
                {
                    AddDir(di, Nodes[Nodes.Count - 1]);

                }
                else if (di.Exists)
                {
                    FileInfo[] fi = di.GetFiles();

                    for (int l = 0; l < fi.Length; l++)
                    {

                        Nodes[Nodes.Count - 1].Nodes.Add(fi[l].FullName, fi[l].Name);

                    }


                }

            }));

        }

        public void RemoveNode(List<string> key)
        {
            for (int i = 0; i < key.Count; i++)
            {
                Nodes.RemoveByKey(key[i]);
                if (shareFolderMap.ContainsKey(key[i]))
                    shareFolderMap.Remove(key[i]);
            }
        }

        private void AddDir(DirectoryInfo di, TreeNode node)
        {


            DirectoryInfo[] directories = di.GetDirectories();
            FileInfo[] files = di.GetFiles();

            //서버에 보낼 노드정보들을 얻기위함/////////////
            
            if (directories.Length > 0)
            {
                nodeList.Add(directories.Length.ToString());
                nodeList.Add(di.FullName);
                for (int i = 0; i < directories.Length; i++)
                {
                    nodeList.Add(directories[i].FullName);
                    nodeList.Add(directories[i].Name);
                }
            }

            
            if (files.Length > 0)
            {
                nodeList.Add(files.Length.ToString());
                nodeList.Add(di.FullName);
                for (int l = 0; l < files.Length; l++)
                {
                    nodeList.Add(files[l].FullName);
                    nodeList.Add(files[l].Name);

                }
            }
            //////////////////////////////////////////

            for (int i = 0; i < directories.Length; i++)
            {
                node.Nodes.Add(directories[i].FullName, directories[i].Name);

                if (di.Exists && di.GetDirectories().Length != 0)
                {
                    AddDir(directories[i], node.Nodes[i]);
                }


            }

            for (int l = 0; l < files.Length; l++)
            {


                node.Nodes.Add(files[l].FullName, files[l].Name);


            }

        }


        private void PrintRecursive(TreeNode node, ref List<string[]> list)
        {
            if (node.Level == 0)
                list.Add(new string[4] { node.Text, node.FullPath, node.Text, node.Name });
            else
            {
                DirectoryInfo di = new DirectoryInfo(node.Name);
                if (!di.Exists)
                    list.Add(new string[4] { node.Parent.Text, node.FullPath, node.Text, node.Name });
            }
            // Print each node recursively.
            foreach (TreeNode tn in node.Nodes)
            {
                PrintRecursive(tn,ref list);
            }
        }

        public void GetChildNodes(TreeNode node, ref List<string[]> list)
        {
            // Print each node recursively.

            if (node.Level == 0)
                list.Add(new string[4] { node.Text, node.FullPath, node.Text, node.Name });
            else
            {
                DirectoryInfo di = new DirectoryInfo(node.Name);
                if(!di.Exists)
                    list.Add(new string[4] { node.Parent.Text, node.FullPath, node.Text, node.Name });
                
                   
            }
            TreeNodeCollection nodes = node.Nodes;
            foreach (TreeNode n in nodes)
            {
                PrintRecursive(n, ref list);
                
            }
        }

        public void InitField(List<String[]> path, Dictionary<String, ShareFolder> map, MainForm form)
        {
            if (isInit)
            {
                form.DoAddText("이미 트리를 초기화 하였습니다");
                return;
            }
            rootFolderPath = path;
            shareFolderMap = map;
            mainForm = form;
         
            isInit = true;
        }

        public string GetParentPath(TreeNode node)
        {
            string serverPath = "";
            if (node.Level == 0)
                serverPath = node.Name;
            DirectoryInfo di = new DirectoryInfo(node.Name);
            if (di.Exists)
                serverPath = node.Name;
            else
                serverPath = node.Parent.Name;

            return serverPath;
        }

        public void FindNodes(TreeNode node, string name, ref List<string[]> list)
        {
            if (node.Text.Equals(name))
                list.Add( new string[2]{ node.Name, node.Text});

            TreeNodeCollection nodes = node.Nodes;
            foreach (TreeNode n in nodes)
            {
                FindRecursive(n, name,ref list);

            }
        }


        private void FindRecursive(TreeNode node, string name, ref List<string[]> list)
        {
            if (node.Text.Equals(name))
                list.Add( new string[2]{node.Name, node.Text});
            // Print each node recursively.
            foreach (TreeNode tn in node.Nodes)
            {
                FindRecursive(tn, name, ref list);
            }
        }

        public static void MakeParentNode(TreeNode node, string fullPath)
        {
            TreeNode oriNode = (TreeNode)node.Clone();
            string[] partial = fullPath.Split('\\');
            string path = "";
            string root = node.Name;
            node.TreeView.Invoke(new Action(delegate()
            {

                for (int i = 0; i < partial.Length; i++)
                {

                    if (oriNode.Nodes.Find(root + "\\" + partial[i], false).Length == 0)
                    {
                        node = node.Nodes.Add(root + "\\" + partial[i], partial[i]);
                        root = node.Name;
                    }
                    else
                        root = root + "\\" + partial[i];
                }

            }));




        }

        private List<TreeNode> m_SelectedNodes = null;
        public List<TreeNode> SelectedNodes
        {
            get
            {
                return m_SelectedNodes;
            }
            set
            {
                ClearSelectedNodes();
                if (value != null)
                {
                    foreach (TreeNode node in value)
                    {
                        ToggleNode(node, true);
                    }
                }
            }
        }

        // Note we use the new keyword to Hide the native treeview's 
        // SelectedNode property.
        private TreeNode m_SelectedNode;
        public new TreeNode SelectedNode
        {
            get
            {
                return m_SelectedNode;
            }
            set
            {
                ClearSelectedNodes();
                if (value != null)
                {
                    SelectNode(value);
                }
            }
        }

        protected override void OnNotifyMessage(Message m)
        {
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            // Make sure at least one node has a selection
            // this way we can tab to the ctrl and use the
            // keyboard to select nodes
            try
            {
                if (m_SelectedNode == null && this.TopNode != null)
                {
                    ToggleNode(this.TopNode, true);
                }

                base.OnGotFocus(e);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            // If the user clicks on a node that was not
            // previously selected, sel0ect it now.
            //prevSelectedNode =(TreeNode) SelectedNode.Clone();
            try
            {
                base.SelectedNode = null;

                TreeNode node = this.GetNodeAt(e.Location);
                if (node != null)
                {
                    //Allow user to click on image
                    int leftBound = node.Bounds.X; // - 20; 
                    // Give a little extra room
                    int rightBound = node.Bounds.Right + 10;
                    if (e.Location.X > leftBound && e.Location.X < rightBound)
                    {
                        if (ModifierKeys ==
                            Keys.None && (m_SelectedNodes.Contains(node)))
                        {
                            // Potential Drag Operation
                            // Let Mouse Up do select
                        }
                        else
                        {
                            SelectNode(node);
                        }
                    }
                }

                base.OnMouseDown(e);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            // If you clicked on a node that WAS previously
            // selected then, reselect it now. This will clear
            // any other selected nodes. e.g. A B C D are selected
            // the user clicks on B, now A C & D are no longer selected.
            try
            {
                // Check to see if a node was clicked on
                TreeNode node = this.GetNodeAt(e.Location);
                if (node != null)
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {

                        SelectedNodes.Clear();
                        SelectedNode = node;

                    }


                    else if (ModifierKeys == Keys.None && m_SelectedNodes.Contains(node))
                    {
                        // Allow user to click on image
                        int leftBound = node.Bounds.X; // - 20; 
                        // Give a little extra room
                        int rightBound = node.Bounds.Right + 10;
                        if (e.Location.X > leftBound && e.Location.X < rightBound)
                        {
                            SelectNode(node);
                        }
                    }
                }

                base.OnMouseUp(e);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            string temp;
            TreeNode dropNode = this.GetNodeAt(this.PointToClient(new Point(drgevent.X, drgevent.Y)));
            if (prevSelectedNode.Level == 0)
            {
                MessageBox.Show("공유폴더를 이동시킬순 없습니다.");
                return;
            }

            int index = moveNodeDirInfo.FullName.LastIndexOf("\\");
            int index1 = moveNodeFileInfo.FullName.LastIndexOf("\\");

            if (moveNodeDirInfo.Exists)
            {
                temp = moveNodeDirInfo.FullName.Substring(index);
                try
                {
                    System.IO.Directory.Move(moveNodeDirInfo.FullName, dropNode.FullPath + temp);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("에러 발생 : " + ex.Message);
                    return;
                }

            }
            if (moveNodeFileInfo.Exists)
            {
                temp = moveNodeFileInfo.FullName.Substring(index1);
                try
                {
                    System.IO.File.Move(moveNodeFileInfo.FullName, dropNode.Name + temp);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("에러 발생 : " + ex.Message);
                    return;
                }
            }

            if (prevSelectedNode.Name.Equals(dropNode.Name) == false)
            {
              
                if (SelectedNode.Parent == null)
                {
                    Nodes.Remove(SelectedNode);
                }
                else
                {

                    prevSelectedNode.Parent.Nodes.Remove(prevSelectedNode);
                    
                }
                 dropNode.Nodes.Add(prevSelectedNode);
  
               
            }

            SelectedNode = null;
            

            base.OnDragDrop(drgevent);
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            // If the user drags a node and the node being dragged is NOT
            // selected, then clear the active selection, select the
            // node being dragged and drag it. Otherwise if the node being
            // dragged is selected, drag the entire selection.

            //SelectedNode = (TreeNode)e.Item;
            prevSelectedNode = (TreeNode) e.Item;
            moveNodeDirInfo = new DirectoryInfo(SelectedNode.Name);

            moveNodeFileInfo = new FileInfo(SelectedNode.Name);

            DoDragDrop(e.Item, DragDropEffects.Move);

          

        }

        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes[0].Text == "*")
            {
                e.Node.Nodes.Clear();
                Fill(e.Node);
            }
            base.OnBeforeExpand(e);
        }

        private void DragScroll(DragEventArgs e)
        {

        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {

            TreeNode dropNode = this.GetNodeAt(this.PointToClient(new Point(drgevent.X, drgevent.Y)));

            if (dropNode == null)
            {
                drgevent.Effect = DragDropEffects.None;
                return;
            }

            drgevent.Effect = DragDropEffects.Move;

            if (Candidate_Node != dropNode)
            {
                SelectedNode = dropNode;
                Candidate_Node = dropNode;
            }

            TreeNode TempNode = dropNode;

            while (TempNode.Parent != null)
            {
                if (TempNode.Parent == SelectedNode) drgevent.Effect = DragDropEffects.None;
                TempNode = TempNode.Parent;
            }

            DragScroll(drgevent);
        }

        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            // Never allow base.SelectedNode to be set!
            try
            {
                base.SelectedNode = null;
                e.Cancel = true;

                base.OnBeforeSelect(e);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            // Never allow base.SelectedNode to be set!
            try
            {
                base.OnAfterSelect(e);
                base.SelectedNode = null;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Handle all possible key strokes for the control.
            // including navigation, selection, etc.

            base.OnKeyDown(e);

            if (e.KeyCode == Keys.ShiftKey) return;

            //this.BeginUpdate();
            bool bShift = (ModifierKeys == Keys.Shift);

            try
            {
                // Nothing is selected in the tree, this isn't a good state
                // select the top node
                if (m_SelectedNode == null && this.TopNode != null)
                {
                    ToggleNode(this.TopNode, true);
                }

                // Nothing is still selected in the tree, 
                // this isn't a good state, leave.
                if (m_SelectedNode == null) return;

                if (e.KeyCode == Keys.Left)
                {
                    if (m_SelectedNode.IsExpanded && m_SelectedNode.Nodes.Count > 0)
                    {
                        // Collapse an expanded node that has children
                        m_SelectedNode.Collapse();
                    }
                    else if (m_SelectedNode.Parent != null)
                    {
                        // Node is already collapsed, try to select its parent.
                        SelectSingleNode(m_SelectedNode.Parent);
                    }
                }
                else if (e.KeyCode == Keys.Right)
                {
                    if (!m_SelectedNode.IsExpanded)
                    {
                        // Expand a collapsed node's children
                        m_SelectedNode.Expand();
                    }
                    else
                    {
                        // Node was already expanded, select the first child
                        SelectSingleNode(m_SelectedNode.FirstNode);
                    }
                }
                else if (e.KeyCode == Keys.Up)
                {
                    // Select the previous node
                    if (m_SelectedNode.PrevVisibleNode != null)
                    {
                        SelectNode(m_SelectedNode.PrevVisibleNode);
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    // Select the next node
                    if (m_SelectedNode.NextVisibleNode != null)
                    {
                        SelectNode(m_SelectedNode.NextVisibleNode);
                    }
                }
                else if (e.KeyCode == Keys.Home)
                {
                    if (bShift)
                    {
                        if (m_SelectedNode.Parent == null)
                        {
                            // Select all of the root nodes up to this point
                            if (this.Nodes.Count > 0)
                            {
                                SelectNode(this.Nodes[0]);
                            }
                        }
                        else
                        {
                            // Select all of the nodes up to this point under 
                            // this nodes parent
                            SelectNode(m_SelectedNode.Parent.FirstNode);
                        }
                    }
                    else
                    {
                        // Select this first node in the tree
                        if (this.Nodes.Count > 0)
                        {
                            SelectSingleNode(this.Nodes[0]);
                        }
                    }
                }
                else if (e.KeyCode == Keys.End)
                {
                    if (bShift)
                    {
                        if (m_SelectedNode.Parent == null)
                        {
                            // Select the last ROOT node in the tree
                            if (this.Nodes.Count > 0)
                            {
                                SelectNode(this.Nodes[this.Nodes.Count - 1]);
                            }
                        }
                        else
                        {
                            // Select the last node in this branch
                            SelectNode(m_SelectedNode.Parent.LastNode);
                        }
                    }
                    else
                    {
                        if (this.Nodes.Count > 0)
                        {
                            // Select the last node visible node in the tree.
                            // Don't expand branches incase the tree is virtual
                            TreeNode ndLast = this.Nodes[0].LastNode;
                            while (ndLast.IsExpanded && (ndLast.LastNode != null))
                            {
                                ndLast = ndLast.LastNode;
                            }
                            SelectSingleNode(ndLast);
                        }
                    }
                }
                else if (e.KeyCode == Keys.PageUp)
                {
                    // Select the highest node in the display
                    int nCount = this.VisibleCount;
                    TreeNode ndCurrent = m_SelectedNode;
                    while ((nCount) > 0 && (ndCurrent.PrevVisibleNode != null))
                    {
                        ndCurrent = ndCurrent.PrevVisibleNode;
                        nCount--;
                    }
                    SelectSingleNode(ndCurrent);
                }
                else if (e.KeyCode == Keys.PageDown)
                {
                    // Select the lowest node in the display
                    int nCount = this.VisibleCount;
                    TreeNode ndCurrent = m_SelectedNode;
                    while ((nCount) > 0 && (ndCurrent.NextVisibleNode != null))
                    {
                        ndCurrent = ndCurrent.NextVisibleNode;
                        nCount--;
                    }
                    SelectSingleNode(ndCurrent);
                }
                else
                {
                    // Assume this is a search character a-z, A-Z, 0-9, etc.
                    // Select the first node after the current node that
                    // starts with this character
                    string sSearch = ((char)e.KeyValue).ToString();

                    TreeNode ndCurrent = m_SelectedNode;
                    while ((ndCurrent.NextVisibleNode != null))
                    {
                        ndCurrent = ndCurrent.NextVisibleNode;
                        if (ndCurrent.Text.StartsWith(sSearch))
                        {
                            SelectSingleNode(ndCurrent);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                this.EndUpdate();
            }
        }

        protected override void OnAfterLabelEdit(NodeLabelEditEventArgs e)
        {
            String key1 = e.Node.FullPath;

            if (mainForm.nodeDialog.keys_list.ContainsKey(key1))
            {
                mainForm.nodeDialog.keys_list.Remove(key1);
            }

            this.BeginInvoke((MethodInvoker)delegate
            {

                mainForm.destKey = e.Node.FullPath;
                int index = mainForm.destKey.LastIndexOf("\\");
                String tempNodeName = mainForm.destKey.Substring(index + 1);

                //textBox1.Text = destKey;
                mainForm.nodeDialog.keys_list.Add(mainForm.destKey, tempNodeName);
                DirectoryManager.FolderNameChange(key1, mainForm.destKey);

            });
            base.OnAfterLabelEdit(e);
        }
    
        private void SelectNode(TreeNode node)
        {
            try
            {
                this.BeginUpdate();

                if (m_SelectedNode == null || ModifierKeys == Keys.Control)
                {
                    // Ctrl+Click selects an unselected node, 
                    // or unselects a selected node.
                    bool bIsSelected = m_SelectedNodes.Contains(node);
                    ToggleNode(node, !bIsSelected);
                }
                else if (ModifierKeys == Keys.Shift)
                {
                    // Shift+Click selects nodes between the selected node and here.
                    TreeNode ndStart = m_SelectedNode;
                    TreeNode ndEnd = node;

                    if (ndStart.Parent == ndEnd.Parent)
                    {
                        // Selected node and clicked node have same parent, easy case.
                        if (ndStart.Index < ndEnd.Index)
                        {
                            // If the selected node is beneath 
                            // the clicked node walk down
                            // selecting each Visible node until we reach the end.
                            while (ndStart != ndEnd)
                            {
                                ndStart = ndStart.NextVisibleNode;
                                if (ndStart == null) break;
                                ToggleNode(ndStart, true);
                            }
                        }
                        else if (ndStart.Index == ndEnd.Index)
                        {
                            // Clicked same node, do nothing
                        }
                        else
                        {
                            // If the selected node is above the clicked node walk up
                            // selecting each Visible node until we reach the end.
                            while (ndStart != ndEnd)
                            {
                                ndStart = ndStart.PrevVisibleNode;
                                if (ndStart == null) break;
                                ToggleNode(ndStart, true);
                            }
                        }
                    }
                    else
                    {
                        // Selected node and clicked node have same parent, hard case.
                        // We need to find a common parent to determine if we need
                        // to walk down selecting, or walk up selecting.

                        TreeNode ndStartP = ndStart;
                        TreeNode ndEndP = ndEnd;
                        int startDepth = Math.Min(ndStartP.Level, ndEndP.Level);

                        // Bring lower node up to common depth
                        while (ndStartP.Level > startDepth)
                        {
                            ndStartP = ndStartP.Parent;
                        }

                        // Bring lower node up to common depth
                        while (ndEndP.Level > startDepth)
                        {
                            ndEndP = ndEndP.Parent;
                        }

                        // Walk up the tree until we find the common parent
                        while (ndStartP.Parent != ndEndP.Parent)
                        {
                            ndStartP = ndStartP.Parent;
                            ndEndP = ndEndP.Parent;
                        }

                        // Select the node
                        if (ndStartP.Index < ndEndP.Index)
                        {
                            // If the selected node is beneath 
                            // the clicked node walk down
                            // selecting each Visible node until we reach the end.
                            while (ndStart != ndEnd)
                            {
                                ndStart = ndStart.NextVisibleNode;
                                if (ndStart == null) break;
                                ToggleNode(ndStart, true);
                            }
                        }
                        else if (ndStartP.Index == ndEndP.Index)
                        {
                            if (ndStart.Level < ndEnd.Level)
                            {
                                while (ndStart != ndEnd)
                                {
                                    ndStart = ndStart.NextVisibleNode;
                                    if (ndStart == null) break;
                                    ToggleNode(ndStart, true);
                                }
                            }
                            else
                            {
                                while (ndStart != ndEnd)
                                {
                                    ndStart = ndStart.PrevVisibleNode;
                                    if (ndStart == null) break;
                                    ToggleNode(ndStart, true);
                                }
                            }
                        }
                        else
                        {
                            // If the selected node is above 
                            // the clicked node walk up
                            // selecting each Visible node until we reach the end.
                            while (ndStart != ndEnd)
                            {
                                ndStart = ndStart.PrevVisibleNode;
                                if (ndStart == null) break;
                                ToggleNode(ndStart, true);
                            }
                        }
                    }
                }
                else
                {
                    // Just clicked a node, select it
                    SelectSingleNode(node);
                }

                OnAfterSelect(new TreeViewEventArgs(m_SelectedNode));
            }
            finally
            {
                this.EndUpdate();
            }
        }

        private void ClearSelectedNodes()
        {
            try
            {
                foreach (TreeNode node in m_SelectedNodes)
                {
                    node.BackColor = this.BackColor;
                    node.ForeColor = this.ForeColor;
                }
            }
            finally
            {
                m_SelectedNodes.Clear();
                m_SelectedNode = null;
            }
        }

        private void SelectSingleNode(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            ClearSelectedNodes();
            ToggleNode(node, true);
            node.EnsureVisible();
        }

        private void ToggleNode(TreeNode node, bool bSelectNode)
        {
            if (bSelectNode)
            {
                m_SelectedNode = node;
                if (!m_SelectedNodes.Contains(node))
                {
                    m_SelectedNodes.Add(node);
                }
                node.BackColor = SystemColors.Highlight;
                node.ForeColor = SystemColors.HighlightText;
            }
            else
            {
                m_SelectedNodes.Remove(node);
                node.BackColor = this.BackColor;
                node.ForeColor = this.ForeColor;

            }
        }

        private void HandleException(Exception ex)
        {
            // Perform some error handling here.
            // We don't want to bubble errors to the CLR.
            MessageBox.Show(ex.Message);
        }

        public void ExpendParent(TreeNode p)
        {

            p.Expand();
            p.Checked = false;
             SelectedNode.BackColor = SystemColors.Highlight;

            if (p.Parent != null)
            {
                ExpendParent(p.Parent);

            }
        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs gfbevent)
        {
            if (gfbevent.Effect == DragDropEffects.Move)
            {
                gfbevent.UseDefaultCursors = false;
                Cursor = Cursors.Default;
            }
            else gfbevent.UseDefaultCursors = true;


            base.OnGiveFeedback(gfbevent);
        }

        public void Fill(TreeNode dirNode)
        {
            DirectoryInfo dir = new DirectoryInfo(dirNode.FullPath);

            try
            {
                foreach (DirectoryInfo dirItem in dir.GetDirectories())
                {
                    TreeNode newNode = new TreeNode(dirItem.Name);
                    dirNode.Nodes.Add(newNode);
                    newNode.Nodes.Add("*");
                }

                FileInfo[] files = dir.GetFiles();

                foreach (FileInfo currFile in files)
                {
                    TreeNode nd = dirNode.Nodes.Add(currFile.Name);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("에러 발생 : " + e.Message);
            }

        }
    }
}
