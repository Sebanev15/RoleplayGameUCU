namespace LibraryTests;
using Program;

public class ElveTests
{
    private Elve elve;
    private Elve elve2;
    private Healing potion;
    private Weapon sword;
    
        [SetUp]
        public void Setup()
        {
            elve = new Elve("Legolas", 100, 10, 20);
            elve2 = new Elve( "Juna", 50, 50, 5);
            potion = new Healing("Poción de Vida", 5, true, 20);
            sword = new Weapon("Espada", 40, 10, true);
        }

        [Test]
        public void AttackFunciona()
        {
            elve.Attack(elve2);
            double expectedDamage = elve.GetTotalDamage() - (elve2.GetTotalDefense() * 0.75);
            if (expectedDamage < 0) expectedDamage = 0;
            Assert.That(elve2.Health, Is.EqualTo(elve2.MaxHealth - expectedDamage));
        }

        [Test]
        public void AttackMataAEnemigo()
        {
            elve2.Health = 10;
            elve2.Defense = 1;
            elve.Attack(elve2);
            Assert.IsFalse(elve2.IsAlive);
        }
        
        [Test]
        public void AttackAEnemigoMuerto()
        {
            elve2.IsAlive = false;
            using (var consoleOutput = new System.IO.StringWriter())
            {
                Console.SetOut(consoleOutput);

                // Intentamos atacarlo
                elve.Attack(elve2);

                // Obtenemos el texto escrito en la consola
                string output = consoleOutput.ToString().Trim();

            Assert.That(output, Is.EqualTo($"ERROR {elve2.Name} ya esta muerto, por lo que no se lo puede atacar"));
            }
        }
            
            [Test]
        public void AddItemAumentaElValorDeAtaqueYDefensaCuandoEsMagico()
        {
            elve.AddItem(sword);
            Assert.That(elve.GetTotalDamage(), Is.EqualTo(100)); 
            Assert.That(elve.GetTotalDefense(), Is.EqualTo(30));
        }

        [Test]
        public void AddItemNoSeDuplica()
        {
            elve.AddItem(sword);
            Assert.That(elve.Items.Count, Is.EqualTo(1));
            elve.AddItem(sword);
            Assert.That(elve.Items.Count, Is.EqualTo(1)); 
        }

        [Test]
        public void RemoveItemFunciona()
        {
            elve.AddItem(sword);
            elve.RemoveItem(sword);
            Assert.IsFalse(elve.Items.Contains(sword));
        }
        
        [Test]
        public void RemoveItemSinElItem()
        {
            using (var consoleOutput = new System.IO.StringWriter())
            {
                Console.SetOut(consoleOutput);

                // Intentamos removerlo
                elve.RemoveItem(sword);

                // Obtenemos el texto escrito en la consola
                string output = consoleOutput.ToString().Trim();

                // Verificamos que contiene el mensaje esperado
                Assert.That(output, Is.EqualTo($"ERROR {elve.Name} no tenia un/a {sword.Name}"));
            }
        }
        
        [Test]
        public void HealCuraNormal()
        {
            elve.AddItem(potion);
            elve2.Health = 30;
            elve.Heal(elve2);
            Assert.That(elve2.Health, Is.EqualTo(50));
        }
        
        [Test]
        public void HealNoExcedeElMaxHealth()
        {
            elve.AddItem(potion);
            elve2.Health = 5;
            elve.Heal(elve2);
            Assert.That(elve2.Health, Is.EqualTo(45));
        }

}