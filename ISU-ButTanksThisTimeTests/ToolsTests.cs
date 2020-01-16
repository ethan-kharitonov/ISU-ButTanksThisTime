using NUnit.Framework;
using Microsoft.Xna.Framework;
using System;

namespace ISU_ButTanksThisTime.Tests
{
    [TestFixture]
    public class ToolsTests
    {
        private static readonly TestCaseData[] rotateToVecTestData =
        {
            new TestCaseData(0, new Vector2(1, 0), 90){ ExpectedResult = 0 },
            new TestCaseData(0, new Vector2(-1, 0), 90){ ExpectedResult = 90 },
            new TestCaseData(0, new Vector2(0, 1), 90){ ExpectedResult = 90 },
            new TestCaseData(0, new Vector2(0, -1), 90){ ExpectedResult = 270 },
            new TestCaseData(315, new Vector2(1, 1), 5){ ExpectedResult = 320 },
            new TestCaseData(270, new Vector2(0, 0), 90){ ExpectedResult = 0 },
            new TestCaseData(185, new Vector2(0, 0), 90){ ExpectedResult = 275 },
            new TestCaseData(175, new Vector2(0, 0), 90){ ExpectedResult = 85 },
            new TestCaseData(89, new Vector2(0, 1), 2){ ExpectedResult = 90 }

        };

        [TestCaseSource(nameof(rotateToVecTestData))]
        public float RotateTowardsVectorTest(float current, Vector2 target, float rotationSpeed)
        {
            return Tools.RotateTowardsVectorTest(current, target, rotationSpeed);
        }
    }
}