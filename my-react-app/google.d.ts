declare namespace google {
    namespace accounts {
      namespace id {
        interface RenderButtonOptions {
          theme: string;
          size: string;
        }
  
        function initialize(options: { client_id: string; callback: (response: any) => void }): void;
        function renderButton(element: HTMLElement, options: RenderButtonOptions): void;
      }
    }
  }