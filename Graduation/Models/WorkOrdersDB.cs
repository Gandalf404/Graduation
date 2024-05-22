namespace Graduation.Models
{
    public static class WorkOrdersDB
    {
        public static Master.WorkOrdersContext graduationContextMaster = new Master.WorkOrdersContext();
        public static Admin.WorkOrdersContext graduationContextAdmin = new Admin.WorkOrdersContext();
    }
}
