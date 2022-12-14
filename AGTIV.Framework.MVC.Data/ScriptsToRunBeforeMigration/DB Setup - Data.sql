SET IDENTITY_INSERT [dbo].[d_tblAction] ON 

INSERT [dbo].[d_tblAction] ([ActionID], [ActionName], [CurStepID], [NextStepID], [MinSlot], [ProcessID]) VALUES (1, N'Approve', 1, 2, 1, 1)
INSERT [dbo].[d_tblAction] ([ActionID], [ActionName], [CurStepID], [NextStepID], [MinSlot], [ProcessID]) VALUES (2, N'Default', 1, 2, 0, 1)
INSERT [dbo].[d_tblAction] ([ActionID], [ActionName], [CurStepID], [NextStepID], [MinSlot], [ProcessID]) VALUES (3, N'Reject', 1, 7, 1, 1)
INSERT [dbo].[d_tblAction] ([ActionID], [ActionName], [CurStepID], [NextStepID], [MinSlot], [ProcessID]) VALUES (4, N'Approve', 2, 3, 0, 1)
INSERT [dbo].[d_tblAction] ([ActionID], [ActionName], [CurStepID], [NextStepID], [MinSlot], [ProcessID]) VALUES (5, N'Default', 2, 3, 0, 1)
INSERT [dbo].[d_tblAction] ([ActionID], [ActionName], [CurStepID], [NextStepID], [MinSlot], [ProcessID]) VALUES (6, N'Reject', 2, 7, 1, 1)
INSERT [dbo].[d_tblAction] ([ActionID], [ActionName], [CurStepID], [NextStepID], [MinSlot], [ProcessID]) VALUES (7, N'Approve', 3, 4, 0, 1)
INSERT [dbo].[d_tblAction] ([ActionID], [ActionName], [CurStepID], [NextStepID], [MinSlot], [ProcessID]) VALUES (8, N'Default', 3, 4, 0, 1)
INSERT [dbo].[d_tblAction] ([ActionID], [ActionName], [CurStepID], [NextStepID], [MinSlot], [ProcessID]) VALUES (9, N'Reject', 3, 7, 1, 1)
INSERT [dbo].[d_tblAction] ([ActionID], [ActionName], [CurStepID], [NextStepID], [MinSlot], [ProcessID]) VALUES (10, N'Approve', 4, 5, 0, 1)
INSERT [dbo].[d_tblAction] ([ActionID], [ActionName], [CurStepID], [NextStepID], [MinSlot], [ProcessID]) VALUES (11, N'Default', 4, 5, 0, 1)
INSERT [dbo].[d_tblAction] ([ActionID], [ActionName], [CurStepID], [NextStepID], [MinSlot], [ProcessID]) VALUES (12, N'Reject', 4, 7, 1, 1)
INSERT [dbo].[d_tblAction] ([ActionID], [ActionName], [CurStepID], [NextStepID], [MinSlot], [ProcessID]) VALUES (13, N'Approve', 5, 6, 0, 1)
INSERT [dbo].[d_tblAction] ([ActionID], [ActionName], [CurStepID], [NextStepID], [MinSlot], [ProcessID]) VALUES (14, N'Default', 5, 6, 0, 1)
INSERT [dbo].[d_tblAction] ([ActionID], [ActionName], [CurStepID], [NextStepID], [MinSlot], [ProcessID]) VALUES (15, N'Reject', 5, 7, 1, 1)
INSERT [dbo].[d_tblAction] ([ActionID], [ActionName], [CurStepID], [NextStepID], [MinSlot], [ProcessID]) VALUES (16, N'Resubmit', 7, 1, 0, 1)
INSERT [dbo].[d_tblAction] ([ActionID], [ActionName], [CurStepID], [NextStepID], [MinSlot], [ProcessID]) VALUES (17, N'Started', 8, 8, 0, 1)
INSERT [dbo].[d_tblAction] ([ActionID], [ActionName], [CurStepID], [NextStepID], [MinSlot], [ProcessID]) VALUES (18, N'GoTo', 10, 10, 0, 1)
SET IDENTITY_INSERT [dbo].[d_tblAction] OFF
SET IDENTITY_INSERT [dbo].[d_tblProcess] ON 

INSERT [dbo].[d_tblProcess] ([ProcessID], [ProcessName], [EncryptParam]) VALUES (2, N'Approval', 0)
SET IDENTITY_INSERT [dbo].[d_tblProcess] OFF
SET IDENTITY_INSERT [dbo].[d_tblStep] ON 

INSERT [dbo].[d_tblStep] ([StepID], [StepName], [InternalStepName], [StepOrder], [DueDateDay], [EmailNotification], [EmailToAssignee], [EmailToOriginator], [EmailCCOriginator], [EmailNotificationSubject], [EmailNotificationBody], [TaskURL], [LastStep], [EmailOnlyStep], [CodeOnlyStep], [AssemblyName], [ClassName], [MethodName], [ProcessID]) VALUES (1, N'Pending Manager', N'Stage1', 1, 3, 1, 1, 0, 0, N'Request for {ReferenceKey} is pending for your approval.', N'<p>Dear {AssigneeName},</p><p> Request by {OriginatorName} is pending for your approval.</p><p> Please click on the following link to review the request:&nbsp;</p><p><a href="https://{TaskLink}" title="" style="text-align: inherit; font-family: Roboto, &quot;Segoe UI&quot;, GeezaPro, &quot;DejaVu Serif&quot;, sans-serif, -apple-system, BlinkMacSystemFont;">{ReferenceKey}</a></p><p> Thank you.</p><p> Warm Regards.</p> <br> <b>----- This is a system generated mail; you are not required to reply this email.  -----</b>', N'{WebURL}Request/ApprovalForm?referenceKey={ReferenceKey}&processId={ProcessID}&taskId={TaskID}', 0, 0, 0, N'AGTIV.Framework.MVC.Business', N'AGTIV.Framework.MVC.Business.Workflows.WorkflowComponent', N'UpdateWFStage', 1)
INSERT [dbo].[d_tblStep] ([StepID], [StepName], [InternalStepName], [StepOrder], [DueDateDay], [EmailNotification], [EmailToAssignee], [EmailToOriginator], [EmailCCOriginator], [EmailNotificationSubject], [EmailNotificationBody], [TaskURL], [LastStep], [EmailOnlyStep], [CodeOnlyStep], [AssemblyName], [ClassName], [MethodName], [ProcessID]) VALUES (2, N'Pending HOD', N'Stage2', 2, 7, 1, 1, 0, 0, N'Request for {ReferenceKey} is pending for your approval.', N'<p>Dear {AssigneeName},</p><p> Request by {OriginatorName} is pending for your approval.</p><p> Please click on the following link to review the request:&nbsp;</p><p><a href="http://{TaskLink}" title="" style="text-align: inherit; font-family: Roboto, &quot;Segoe UI&quot;, GeezaPro, &quot;DejaVu Serif&quot;, sans-serif, -apple-system, BlinkMacSystemFont;">{ReferenceKey}</a></p><p> Thank you.</p><p> Warm Regards.</p> <br> <b>----- This is a system generated mail; you are not required to reply this email.  -----</b>', N'{WebURL}Request/ApprovalForm?referenceKey={ReferenceKey}&processId={ProcessID}&taskId={TaskID}', 0, 0, 0, N'AGTIV.Framework.MVC.Business', N'AGTIV.Framework.MVC.Business.Workflows.WorkflowComponent', N'UpdateWFStage', 1)
INSERT [dbo].[d_tblStep] ([StepID], [StepName], [InternalStepName], [StepOrder], [DueDateDay], [EmailNotification], [EmailToAssignee], [EmailToOriginator], [EmailCCOriginator], [EmailNotificationSubject], [EmailNotificationBody], [TaskURL], [LastStep], [EmailOnlyStep], [CodeOnlyStep], [AssemblyName], [ClassName], [MethodName], [ProcessID]) VALUES (3, N'Pending MD', N'Stage3', 3, 0, 1, 1, 0, 0, N'Request for {ReferenceKey} is pending for your approval.', N'<p>Dear {AssigneeName},</p><p> Request by {OriginatorName} is pending for your approval.</p><p> Please click on the following link to review the request:&nbsp;</p><p><a href="http://{TaskLink}" title="" style="text-align: inherit; font-family: Roboto, &quot;Segoe UI&quot;, GeezaPro, &quot;DejaVu Serif&quot;, sans-serif, -apple-system, BlinkMacSystemFont;">{ReferenceKey}</a></p><p> Thank you.</p><p> Warm Regards.</p> <br> <b>----- This is a system generated mail; you are not required to reply this email.  -----</b>', N'{WebURL}Request/ApprovalForm?referenceKey={ReferenceKey}&processId={ProcessID}&taskId={TaskID}', 0, 0, 0, N'AGTIV.Framework.MVC.Business', N'AGTIV.Framework.MVC.Business.Workflows.WorkflowComponent', N'UpdateWFStage', 1)
INSERT [dbo].[d_tblStep] ([StepID], [StepName], [InternalStepName], [StepOrder], [DueDateDay], [EmailNotification], [EmailToAssignee], [EmailToOriginator], [EmailCCOriginator], [EmailNotificationSubject], [EmailNotificationBody], [TaskURL], [LastStep], [EmailOnlyStep], [CodeOnlyStep], [AssemblyName], [ClassName], [MethodName], [ProcessID]) VALUES (4, N'Stage 4', N'Stage4', 4, 0, 1, 1, 0, 0, N'Request for {ReferenceKey} is pending for your approval.', N'<p>Dear {AssigneeName},</p><p> Request by {OriginatorName} is pending for your approval.</p><p> Please click on the following link to review the request:&nbsp;</p><p><a href="http://{TaskLink}" title="" style="text-align: inherit; font-family: Roboto, &quot;Segoe UI&quot;, GeezaPro, &quot;DejaVu Serif&quot;, sans-serif, -apple-system, BlinkMacSystemFont;">{ReferenceKey}</a></p><p> Thank you.</p><p> Warm Regards.</p> <br> <b>----- This is a system generated mail; you are not required to reply this email.  -----</b>', N'{WebURL}Request/ApprovalForm?referenceKey={ReferenceKey}&processId={ProcessID}&taskId={TaskID}', 0, 0, 0, N'AGTIV.Framework.MVC.Business', N'AGTIV.Framework.MVC.Business.Workflows.WorkflowComponent', N'UpdateWFStage', 1)
INSERT [dbo].[d_tblStep] ([StepID], [StepName], [InternalStepName], [StepOrder], [DueDateDay], [EmailNotification], [EmailToAssignee], [EmailToOriginator], [EmailCCOriginator], [EmailNotificationSubject], [EmailNotificationBody], [TaskURL], [LastStep], [EmailOnlyStep], [CodeOnlyStep], [AssemblyName], [ClassName], [MethodName], [ProcessID]) VALUES (5, N'Stage 5', N'Stage5', 5, 0, 1, 1, 0, 0, N'Request for {ReferenceKey} is pending for your approval.', N'<p>Dear {AssigneeName},</p><p> Request by {OriginatorName} is pending for your approval.</p><p> Please click on the following link to review the request:&nbsp;</p><p><a href="http://{TaskLink}" title="" style="text-align: inherit; font-family: Roboto, &quot;Segoe UI&quot;, GeezaPro, &quot;DejaVu Serif&quot;, sans-serif, -apple-system, BlinkMacSystemFont;">{ReferenceKey}</a></p><p> Thank you.</p><p> Warm Regards.</p> <br> <b>----- This is a system generated mail; you are not required to reply this email.  -----</b>', N'{WebURL}Request/ApprovalForm?referenceKey={ReferenceKey}&processId={ProcessID}&taskId={TaskID}', 0, 0, 0, N'AGTIV.Framework.MVC.Business', N'AGTIV.Framework.MVC.Business.Workflows.WorkflowComponent', N'UpdateWFStage', 1)
INSERT [dbo].[d_tblStep] ([StepID], [StepName], [InternalStepName], [StepOrder], [DueDateDay], [EmailNotification], [EmailToAssignee], [EmailToOriginator], [EmailCCOriginator], [EmailNotificationSubject], [EmailNotificationBody], [TaskURL], [LastStep], [EmailOnlyStep], [CodeOnlyStep], [AssemblyName], [ClassName], [MethodName], [ProcessID]) VALUES (6, N'Approved', N'Approved', 6, 0, 1, 0, 1, 0, N'Request for {ReferenceKey} is approved.', N'<p>Dear {OriginatorName},</p><p><br></p><p>
Request with Ref. No. {ReferenceKey} is approved.</p><p>
Please click on the following link to view the request:</p>
<a href="http://{WebURL}/Request/ViewForm?ID={ReferenceKey}" title="">{ReferenceKey}</a><br>
<br><p>
Thank you.</p><p>
Warm Regards.</p>
<br>
<b>----- This is a system generated mail; you are not required to reply this email.&nbsp; -----</b>', N'', 1, 0, 0, N'AGTIV.Framework.MVC.Business', N'AGTIV.Framework.MVC.Business.Workflows.WorkflowComponent', N'UpdateWFStage', 1)
INSERT [dbo].[d_tblStep] ([StepID], [StepName], [InternalStepName], [StepOrder], [DueDateDay], [EmailNotification], [EmailToAssignee], [EmailToOriginator], [EmailCCOriginator], [EmailNotificationSubject], [EmailNotificationBody], [TaskURL], [LastStep], [EmailOnlyStep], [CodeOnlyStep], [AssemblyName], [ClassName], [MethodName], [ProcessID]) VALUES (7, N'Rejected', N'Rejected', 7, 0, 1, 0, 1, 0, N'Request for {ReferenceKey} is rejected and requires your further action.', N'<p>Dear {OriginatorName},</p><br><p>
Request with Ref. No. {ReferenceKey} is rejected. Please resubmit as soon as possible.</p><p>
Please click on the following link for amendments:</p><br>
<a href="http://{TaskLink}" title="">{ReferenceKey}</a><br>
<br><p>
Thank you.</p><p>
Warm Regards.</p>
<br>
<b>----- This is a system generated mail; you are not required to reply this email.&nbsp; -----</b>', N'{WebURL}/Request/ViewForm?ID={ReferenceKey}&processId={ProcessID}&taskId={TaskID}}', 0, 0, 0, N'AGTIV.Framework.MVC.Business', N'AGTIV.Framework.MVC.Business.Workflows.WorkflowComponent', N'UpdateWFStage', 1)
INSERT [dbo].[d_tblStep] ([StepID], [StepName], [InternalStepName], [StepOrder], [DueDateDay], [EmailNotification], [EmailToAssignee], [EmailToOriginator], [EmailCCOriginator], [EmailNotificationSubject], [EmailNotificationBody], [TaskURL], [LastStep], [EmailOnlyStep], [CodeOnlyStep], [AssemblyName], [ClassName], [MethodName], [ProcessID]) VALUES (8, N'Start', N'Start', 10, 0, 0, 0, 0, 0, N' ', N' ', N' ', 0, 0, 0, NULL, NULL, NULL, 1)
INSERT [dbo].[d_tblStep] ([StepID], [StepName], [InternalStepName], [StepOrder], [DueDateDay], [EmailNotification], [EmailToAssignee], [EmailToOriginator], [EmailCCOriginator], [EmailNotificationSubject], [EmailNotificationBody], [TaskURL], [LastStep], [EmailOnlyStep], [CodeOnlyStep], [AssemblyName], [ClassName], [MethodName], [ProcessID]) VALUES (9, N'Error', N'Error', 10, 0, 0, 0, 0, 0, N' ', N' ', N' ', 1, 0, 0, NULL, NULL, NULL, 1)
INSERT [dbo].[d_tblStep] ([StepID], [StepName], [InternalStepName], [StepOrder], [DueDateDay], [EmailNotification], [EmailToAssignee], [EmailToOriginator], [EmailCCOriginator], [EmailNotificationSubject], [EmailNotificationBody], [TaskURL], [LastStep], [EmailOnlyStep], [CodeOnlyStep], [AssemblyName], [ClassName], [MethodName], [ProcessID]) VALUES (10, N'GoTo', N'GoTo', 10, 0, 0, 0, 0, 0, N' ', N' ', N' ', 0, 0, 0, NULL, NULL, NULL, 1)
INSERT [dbo].[d_tblStep] ([StepID], [StepName], [InternalStepName], [StepOrder], [DueDateDay], [EmailNotification], [EmailToAssignee], [EmailToOriginator], [EmailCCOriginator], [EmailNotificationSubject], [EmailNotificationBody], [TaskURL], [LastStep], [EmailOnlyStep], [CodeOnlyStep], [AssemblyName], [ClassName], [MethodName], [ProcessID]) VALUES (11, N'Terminated', N'Terminated', 10, 0, 0, 0, 0, 0, N'', N'', N'', 1, 0, 0, N'AGTIV.Framework.MVC.Business', N'AGTIV.Framework.MVC.Business.Workflows.WorkflowComponent', N'UpdateWFStage', 1)
SET IDENTITY_INSERT [dbo].[d_tblStep] OFF

INSERT [dbo].[AppSecret] ([Id], [AppName], [ClientSecret], [CreatedOn]) VALUES (N'9ace34a8-6f07-4333-b506-032071879485', N'AGTIV Custom MVC', N'aAxlHhnK/xfpqo0wZsVL1Ypq7+k=', CAST(N'2020-04-24T08:02:14.583' AS DateTime))