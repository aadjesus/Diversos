/*
 * Created on 12/06/2005
 *
 */
package examples.apps;

/**
 * @author Roberto Elvira
 * Voice Technology Ind.<br>
 *
 */
public interface CallTrekControlListener {
    /**
     * Notifica que a chamada selecionada mudou.
     * O listener deve verificar na instância de CallTrekControl
     * a nova chamada selecionada.
     * @param arg
     * CallTrekControl notificador da mensagem.
     */
    void onSelectedCallChanged(CallTrekControl arg);
}
