using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;

namespace Services.IAP
{
    internal class IAPService : IStoreListener
    {
        private static IAPService _instance;

        public static IAPService Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new IAPService();
                }
                return _instance;
            }
        }


        private ProductLibrary _productLibrary;

        public event Action Initialized;
        public event Action PurchaseSucceed;
        public event Action PurchaseFailed;

        private bool _isInitialized;
        private IStoreController _controller;
        private IExtensionProvider _extensionProvider;
        private PurchaseValidator _purchaseValidator;
        private PurchaseRestorer _purchaseRestorer;


        public void InitializeProducts(ProductLibrary productLibrary)
        {
            _productLibrary = productLibrary;
            StandardPurchasingModule purchasingModule = StandardPurchasingModule.Instance();
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(purchasingModule);

            foreach (Product product in _productLibrary.Products)
                builder.AddProduct(product.Id, product.ProductType);

            Log("Products initialized");
            UnityPurchasing.Initialize(this, builder);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensionsProvider)
        {
            _isInitialized = true;
            _controller = controller;
            _extensionProvider = extensionsProvider;
            _purchaseValidator = new PurchaseValidator();
            _purchaseRestorer = new PurchaseRestorer(_extensionProvider);

            Log("Initialized");
            Initialized?.Invoke();
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            _isInitialized = false;
            Error("Initialization Failed");
        }


        public void Buy(string id)
        {
            if (!_isInitialized)
            {
                Error($"Buy {id} FAIL. Not initialized.");
                return;
            }

            _controller.InitiatePurchase(id);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            if (_purchaseValidator.Validate(args) == false)
            {
                OnPurchaseFailed(args.purchasedProduct.definition.id, "NonValid");
                return PurchaseProcessingResult.Complete;
            }

            PurchaseSucceed.Invoke();
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureReason failureReason) =>
            OnPurchaseFailed(product.definition.id, failureReason.ToString());

        private void OnPurchaseFailed(string productId, string reason)
        {
            Error($"Failed {productId}: {reason}");
            PurchaseFailed?.Invoke();
        }


        public string GetCost(string productID)
        {
            UnityEngine.Purchasing.Product product = _controller.products.WithID(productID);

            if (product != null)
                return product.metadata.localizedPriceString;

            return "N/A";
        }

        public void RestorePurchases()
        {
            if (!_isInitialized)
            {
                Error("RestorePurchases FAIL. Not initialized.");
                return;
            }

            _purchaseRestorer.Restore();
        }


        private void Log(string message) => Debug.Log(WrapMessage(message));
        private void Error(string message) => Debug.LogError(WrapMessage(message));
        private string WrapMessage(string message) => $"[{GetType().Name}] {message}";
    }
}
