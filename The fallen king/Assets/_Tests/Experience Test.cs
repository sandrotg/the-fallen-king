using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor.SceneManagement;

[TestFixture]
public class ExperienceTest
{
    [SetUp]
    public void SetUp()
    {
        EditorSceneManager.LoadScene("Catacombs");
    }


    [Test]
    public void AddExperience_IncreasesCurrentExp()
    {
        // Arrange
        LevelController levelController = new LevelController();
        int initialExp = levelController.currentExp;
        int expToAdd = 10;
        // Act
        levelController.AddExperience(expToAdd);
        // Assert
        Assert.AreEqual(initialExp + expToAdd, levelController.currentExp);
    }
}
