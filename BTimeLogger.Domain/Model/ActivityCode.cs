using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTimeLogger;

/// <summary>
/// Used to uniquely identify <see cref="Activity"/> objects.
/// </summary>
public class ActivityCode
{
	private const char DELIM = '/';

	private ActivityCode(string value)
	{
		Value = value;
	}

	/// <summary>
	/// Create a code for the given <paramref name="activity"/>.
	/// </summary>
	public static ActivityCode CreateCode(Activity activity)
	{
		if (activity == null) throw new ArgumentNullException(nameof(activity));

		string activityName = activity.Name;
		string[] groupNames = GetGroupNames(activity);
		return CreateCode(activityName, groupNames);
	}

	/// <summary>
	/// Create a code for an <see cref="Activity"/> with the <paramref name="activityName"/> name and <paramref name="groupNames"/> 
	/// group names, where <paramref name="groupNames"/> is an ordered array of parent group names, going from most removed to least
	/// removed (Ex. { "GrandparentName", "ParentName" })
	/// </summary>
	public static ActivityCode CreateCode(string activityName, string[] groupNames)
	{
		if (activityName == null) throw new ArgumentNullException(nameof(activityName));
		if (groupNames == null) throw new ArgumentNullException(nameof(groupNames));

		StringBuilder builder = new();
		for (int i = 0; i < groupNames.Length; i++)
		{
			builder.Append(groupNames[i]);
			builder.Append(DELIM);
		}
		builder.Append(activityName);
		return new ActivityCode(builder.ToString());
	}

	public static ActivityCode CreateCode(string codeValue)
	{
		if (string.IsNullOrEmpty(codeValue) ||
			!ValueFormatIsCorrect(codeValue))
			throw new ArgumentException(nameof(codeValue));

		return new ActivityCode(codeValue);
	}

	private static bool ValueFormatIsCorrect(string codeValue)
	{
		// TODO Issue #2: Implement ValueFormatIsCorrect
		return true;
	}

	public string Value { get; private set; }

	public string[] Parts
	{
		get => string.IsNullOrWhiteSpace(Value)
			? Array.Empty<string>()
			: Value.Split(DELIM);
	}

	public string ActivityName
	{
		get
		{
			if (Parts.Length <= 0) return string.Empty;
			else return Parts[Parts.Length - 1];
		}
	}

	public string[] GroupNames
	{
		get
		{
			if (Parts.Length <= 0) return Array.Empty<string>();
			return Parts.Take(Parts.Length - 1).ToArray();
		}
	}

	public ActivityCode ParentCode
	{
		get
		{
			if (GroupNames.Length <= 0)
				return null;
			string[] groupNames = GroupNames;

			string parentName = groupNames[groupNames.Length - 1];
			string[] parentParentNames = groupNames.Take(groupNames.Length - 1).ToArray();
			return CreateCode(parentName, parentParentNames);
		}
	}

	/// <summary>
	/// Get the complete ancestry of this <see cref="ActivityCode"/>, including this activity code.
	/// </summary>
	public IEnumerable<ActivityCode> AncestorCodes
	{
		get
		{
			if (Parts.Length <= 0)
				return Array.Empty<ActivityCode>();

			List<ActivityCode> ancestors = new();

			ActivityCode currentAncestor = this;
			while (currentAncestor != null)
			{
				ancestors.Add(currentAncestor);
				currentAncestor = currentAncestor.ParentCode;
			}

			return ancestors;
		}
	}

	public override bool Equals(object obj)
	{
		ActivityCode other = obj as ActivityCode;
		if (other == null) return false;

		return other.Value.Equals(Value);
	}

	public override int GetHashCode()
	{
		return Value.GetHashCode();
	}

	public override string ToString()
	{
		return Value;
	}

	private static string[] GetGroupNames(Activity activity)
	{
		List<string> groupNames = new();

		Activity currentGroupAncestor = activity.Parent;
		while (currentGroupAncestor != null)
		{
			groupNames.Add(currentGroupAncestor.Name);
			currentGroupAncestor = currentGroupAncestor.Parent;
		}

		groupNames.Reverse();
		return groupNames.ToArray();
	}
}
