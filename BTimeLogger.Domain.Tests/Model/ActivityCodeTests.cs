using NUnit.Framework;
using System;
using System.Linq;

namespace BTimeLogger.Domain.Tests.Model
{
	public class ActivityCodeTests
	{
		private ActivityCode _code;
		private Activity _defaultActivity;
		private Activity _activityNoParent;
		private Activity _activityWithParent;
		private string _activityName;
		private string _parentName;
		private string[] _groupNames;
		private string _groupNamesValue;
		private ActivityCode _groupNamesCode;

		[SetUp]
		public void SetUp()
		{
			_defaultActivity = CreateDefaultActivity();
			_activityNoParent = CreateActivityWithSingleName();
			_activityWithParent = CreateActivityWithParent();
			_activityName = "ACTIVITY NAME";
			_parentName = "PARENT NAME";
			_groupNames = new string[] { "Grandparent", "Parent" };
			_groupNamesValue = "Grandparent/Parent";
			_groupNamesCode = ActivityCode.CreateCode("Parent", new string[] { "Grandparent" });

		}

		[Test]
		public void CreateCode_NullActivity_ThrowsArgumentNull()
		{
			AssertThrowsArgumentNull(() => CreateCode_NullActivity());
		}

		[Test]
		public void CreateCode_NullValue_ThrowsArgumentNull()
		{
			Assert.Fail(); // TODO
		}

		[Test]
		public void CreateCode_EmptyValue_ThrowsArgumentException()
		{
			Assert.Fail(); // TODO
		}

		[Test]
		public void CreateCode_InvalidFormatValue_ThrowsArgumentException()
		{
			Assert.Fail(); // TODO
		}

		[Test]
		public void CreateCode_ValidFormatValue_CreatesCode()
		{
			Assert.Fail(); // TODO
		}

		[Test]
		public void CreateCode_DefaultActivity_CodeIsEmpty()
		{
			CreateCodeWithActivity(_defaultActivity);

			AssertValueEqualTo(string.Empty);
			AssertXNumParts(0);

			AssertActivityNameEqualTo(string.Empty);
			AssertParentCodeEqualTo(null);
			AssertXNumAncestorCodes(0);
		}

		[Test]
		public void CreateCode_ActivityNoParent_InitializesValue()
		{
			CreateCodeWithActivity(_activityNoParent);

			AssertValueEqualTo(_activityName);
			AssertXNumParts(1);
			AssertActivityNameEqualTo(_activityName);
			AssertParentCodeEqualTo(null);
			AssertXNumAncestorCodes(1);
		}

		[Test]
		public void CreateCode_ActivityWithParent_InitializesValues()
		{
			CreateCodeWithActivity(_activityWithParent);

			AssertValueEqualTo($"{_parentName}/{_activityName}");
			AssertXNumParts(2);
			AssertActivityNameEqualTo(_activityName);

			AssertParentCodeEqualTo(_activityWithParent.Parent.Code);

			AssertXNumAncestorCodes(2);
		}

		[Test]
		public void CreateCode_NullActivityName_ThrowsArgumentNull()
		{
			AssertThrowsArgumentNull(() => CreateCode_NullActivityName());
		}

		[Test]
		public void CreateCode_NullGroupNamesName_ThrowsArgumentNull()
		{
			AssertThrowsArgumentNull(() => CreateCode_NullGroupNames());
		}

		[Test]
		public void CreateCode_ActivityName_InitializesValue()
		{
			CreateCode_ActivityName();

			AssertValueEqualTo(_activityName);
			AssertXNumParts(1);
			AssertActivityNameEqualTo(_activityName);

			AssertParentCodeEqualTo(null);

			AssertXNumAncestorCodes(1);
		}

		[Test]
		public void CreateCode_ActivityName_And_GroupNames_InitializesValues()
		{
			CreateCode_ActivityName_And_GroupNames();

			AssertValueEqualTo($"{_groupNamesValue}/{_activityName}");

			AssertXNumParts(_groupNames.Length + 1);

			AssertActivityNameEqualTo(_activityName);

			AssertParentCodeEqualTo(_groupNamesCode);

			AssertXNumAncestorCodes(_groupNames.Length + 1);
		}

		[Test]
		public void Equals_SameObject_ReturnsTrue()
		{
			ActivityCode code = ActivityCode.CreateCode(_activityWithParent);

			Assert.AreEqual(code, code);
		}

		[Test]
		public void Equals_DiffObject_SameValue_ReturnsTrue()
		{
			ActivityCode code1 = ActivityCode.CreateCode(_activityWithParent);
			ActivityCode code2 = ActivityCode.CreateCode(_activityWithParent);

			Assert.AreNotSame(code1, code2);
			Assert.AreEqual(code1, code2);
		}

		[Test]
		public void Equals_DiffValue_ReturnsFalse()
		{
			ActivityCode code1 = ActivityCode.CreateCode(_activityWithParent);
			ActivityCode code2 = ActivityCode.CreateCode(_activityNoParent);

			Assert.AreNotEqual(code1, code2);
		}

		[Test]
		public void ToString_ReturnsValue()
		{
			CreateActivityWithParent();

			Assert.AreEqual(_code.Value, _code.ToString());
		}

		private void CreateCode_NullActivityName()
		{
			_code = ActivityCode.CreateCode(null, Array.Empty<string>());
		}

		private void CreateCode_NullGroupNames()
		{
			_code = ActivityCode.CreateCode(_activityName, null);
		}

		private Activity CreateActivityWithParent()
		{
			Activity parent = new Activity()
			{
				IsGroup = true,
				Name = _parentName
			};
			Activity child = new()
			{
				Parent = parent,
				Name = _activityName,
				IsGroup = false
			};
			parent.Children.Add(child);

			return child;
		}

		private Activity CreateActivityWithSingleName()
		{
			return new Activity()
			{
				Name = _activityName
			};
		}

		private void AssertThrowsArgumentNull(Action a)
		{
			Assert.Throws<ArgumentNullException>(() => a?.Invoke());
		}

		private void CreateCode_NullActivity()
		{
			_code = ActivityCode.CreateCode((Activity) null);
		}

		private void CreateCode_ActivityName()
		{
			_code = ActivityCode.CreateCode(_activityName, Array.Empty<string>());
		}

		private void CreateCode_ActivityName_And_GroupNames()
		{
			_code = ActivityCode.CreateCode(_activityName, _groupNames);
		}

		private void AssertXNumParts(int x)
		{
			Assert.AreEqual(x, _code.Parts.Length);
		}

		private void AssertValueEqualTo(string expected)
		{
			Assert.AreEqual(expected, _code.Value);
		}

		private void CreateCodeWithActivity(Activity defaultAct)
		{
			_code = ActivityCode.CreateCode(defaultAct);
		}

		private Activity CreateDefaultActivity()
		{
			return new Activity();
		}

		private void AssertParentCodeEqualTo(ActivityCode expectedActivityCode)
		{
			Assert.AreEqual(expectedActivityCode, _code.ParentCode);
		}

		private void AssertXNumAncestorCodes(int x)
		{
			Assert.AreEqual(x, _code.AncestorCodes.Count());
		}

		private void AssertActivityNameEqualTo(string expectedName)
		{
			Assert.AreEqual(expectedName, _code.ActivityName);
		}
	}
}
