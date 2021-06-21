using Godot;
using Godot.Collections;

public static class GroupUtils
{
    public static Array FindNodeDescendantsInGroup(Node node, string groupName)
    {
        Array children = node.GetChildren();
        Array nodesInGroup = new Array();
        foreach (Node item in children)
        {
            if (item.IsInGroup(groupName))
            {
                nodesInGroup.Add(item);
            }
        }
        return nodesInGroup;
    }
}