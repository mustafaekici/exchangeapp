using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EFSqlDependencyHelper.SqlServerNotifier
{

    public delegate void SqlNotificationEventHandler(object sender, SqlNotificationEventArgs e);
 
}