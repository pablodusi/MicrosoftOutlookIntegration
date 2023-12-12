using MicrosoftOutlookIntegration.Interfaces;

public static class OutlookPermissionService
{
    //private static readonly IUserRepository _userRepository = DependencyResolver.GetService<IUserRepository>();

    public static void GrantPermissions(string userId)
    {
        //var user = await _userRepository.GetUserAsync(userId);

        //if (user.IsAdmin)
        //{
        //    var group = await _userRepository.GetGroupAsync("OutlookPermissions");
        //    await _userRepository.AddGroupMemberAsync(user, group);
        //}
    }

    public static void RevokePermissions(string userId)
    {
        //// Obtener el usuario del sistema
        //var user = await _userRepository.GetUserAsync(userId);

        //// ... Revocar permisos ...

        //// Por ejemplo, se puede eliminar el usuario de un grupo de permisos
        //var group = await _userRepository.GetGroupAsync("OutlookPermissions");
        //await _userRepository.RemoveGroupMemberAsync(user, group);
    }
}

