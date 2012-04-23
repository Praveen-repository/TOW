using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Code.Presentation;

namespace Web.Controls
{
    public partial class UCShowPaging : System.Web.UI.UserControl
    {
        public event CustomPagingDelegate.PageChangedEventHandler PageChanged;


        private int _currentPageNumber;
        public int CurrentPageNumber
        {
            get
            {
                if (!string.IsNullOrEmpty(ddlPageNumber.SelectedValue))
                    return int.Parse(ddlPageNumber.SelectedValue);
                else
                    return 0;
            }
            set { _currentPageNumber = value; }
        }

        private int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }

        private int _totalRecords;
        public int TotalRecords
        {
            get { return Convert.ToInt32(this.lblTotalRecords.Text); }
            set { this.lblTotalRecords.Text = value.ToString(); }
        }

        private int _currentPageSize;
        public int CurrentPageSize
        {
            get { return int.Parse(ddlPageSize.SelectedValue); }
            set { _currentPageSize = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TotalPages = TotalRecords % CurrentPageSize == 0 ? TotalRecords / CurrentPageSize : TotalRecords / CurrentPageSize + 1;
                lblTotalRecords.Text = string.Format(" {0} ", this.TotalRecords.ToString());

                ddlPageNumber.Items.Clear();
                for (int count = 1; count <= this.TotalPages; ++count)
                    ddlPageNumber.Items.Add(count.ToString());

                if (TotalPages > 0)
                    ddlPageNumber.Items[0].Selected = true;

                lblShowRecords.Text = string.Format(" {0} ", this.TotalPages.ToString());
            }
        }

        public void ResetPageNumber()
        {
            this.ddlPageNumber.SelectedIndex = 0;
        }


        public void ResetRecordCount(int TotalRecords)
        {
            TotalPages = TotalRecords % CurrentPageSize == 0 ? TotalRecords / CurrentPageSize : TotalRecords / CurrentPageSize + 1;
            int previousPageIndex = 1;
            if (!string.IsNullOrEmpty(ddlPageNumber.SelectedValue))
                previousPageIndex = int.Parse(ddlPageNumber.SelectedValue);

            ddlPageNumber.Items.Clear();
            for (int count = 1; count <= this.TotalPages; ++count)
                ddlPageNumber.Items.Add(count.ToString());

            if (TotalPages > 0)
            {
                if (previousPageIndex > TotalPages)
                    ddlPageNumber.Items[TotalPages - 1].Selected = true;
                else
                    ddlPageNumber.Items[previousPageIndex - 1].Selected = true;
            }
            lblTotalRecords.Text = string.Format(" {0} ", this.TotalRecords.ToString());
            lblShowRecords.Text = string.Format(" {0} ", this.TotalPages.ToString());
        }

        //First DropDownList, for selecting different page number

        protected void ddlPageNumber_SelectedIndexChanged(object sender,
           EventArgs e)
        {
            CustomPageChangeArgs args = new CustomPageChangeArgs();
            args.CurrentPageSize =
               Convert.ToInt32(this.ddlPageSize.SelectedItem.Value);
            args.CurrentPageNumber =
               Convert.ToInt32(this.ddlPageNumber.SelectedItem.Text);
            args.TotalPages = Convert.ToInt32(this.lblShowRecords.Text);
            Pager_PageChanged(this, args);

            lblShowRecords.Text = string.Format(" {0} ",
               args.TotalPages.ToString());
        }

        //second DropDonwList, to change the pagesize of gridView

        protected void ddlPageSize_SelectedIndexChanged(object sender,
           EventArgs e)
        {
            CustomPageChangeArgs args = new CustomPageChangeArgs();
            args.CurrentPageSize =
               Convert.ToInt32(this.ddlPageSize.SelectedItem.Value);
            args.CurrentPageNumber = 1;

            TotalPages = TotalRecords % CurrentPageSize == 0 ? TotalRecords / CurrentPageSize : TotalRecords / CurrentPageSize + 1;

            args.TotalPages = TotalPages; // Convert.ToInt32(this.lblShowRecords.Text);
            Pager_PageChanged(this, args);

            ddlPageNumber.Items.Clear();
            for (int count = 1; count <= this.TotalPages; ++count)
                ddlPageNumber.Items.Add(count.ToString());


            if (ddlPageNumber.Items.Count > 0)
                ddlPageNumber.Items[0].Selected = true;
            lblShowRecords.Text = string.Format(" {0} ", this.TotalPages.ToString());
        }

        void Pager_PageChanged(object sender, CustomPageChangeArgs e)
        {
            if (PageChanged != null)
                PageChanged(this, e);
        }
    }
}